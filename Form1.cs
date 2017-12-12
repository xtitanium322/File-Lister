using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input; // for keyboard events
using System.Collections;   // for Icomparer

/*
 This program is designed to check all php files in the provided directory.
 Files are checked for containing user specified text in order to find, for example, all files
 containing an update statement for a particular table. This way if an update to insert statement is needed, no files will be forgotten.
 
 File names for matching files are listed in the special section.

 * Features:
 * ------------------------------------------------------------------
 * directory name label (shows current active directory of the file checker) 
 * listing of all the files in the directory
 * input field for the user to type in prompts for file checking
 * output window where all the matching file names are shown
 * subfolder check enabled/disabled
 * quick file open option (opens in the associated editor based on file type)
 * status label - displays relevant meta info: number of matching files, clipboard status
 * foldername display in the results list
 * 
 * Possible new options:
 * ------------------------------------------------------------------
 *
 */
namespace Php_File_Lister
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog folder; // selected folder
        DirectoryInfo filedir;      // file directory object
        List<FileInfo> matching_files;    // the listbox only contains filenames - this contains all the detail
        SearchOption subfolders_included = SearchOption.TopDirectoryOnly; // default - is changed by user checking the checkbox
        Logger log = Logger.get_instance(new lq[] { lq.critical_message, lq.system_message, lq.repeating_operation });
        private string last_opened_directory = "";
        private int sortColumn = -1;
        private int number_of_checked_items = 0;
        private theme current_theme;
        /// <summary>
        /// Constructor for the form
        /// </summary>
        public Form1()
        {
            log.Log(lq.system_message, "initializing components", new string[] { "constructor" });
                InitializeComponent();
                folder = new FolderBrowserDialog();                 // folder initialization
                this.FormBorderStyle = FormBorderStyle.FixedSingle; // turn off window resizing
                matching_files = new List<FileInfo>();
                last_opened_directory = get_last_opened_folder(); // restore last folder from file
                current_theme = theme.light;

                if(!last_opened_directory.Equals(""))
                {
                    label_previous_folder_name.Text = last_opened_directory;     // assign previous folder name to label for visibility
                    button_open_previous.Enabled = true;                         // enable the button
                }
        }

        /// <summary>
        /// Function to select a folder for file listing and checking
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters </param>
        private void button_open_folder_Click(object sender, EventArgs e)
        {
            log.Log(lq.system_message , "user clicked on the choose folder button - attempt to open the folder using dialog", new string[] { "user input" });
                ChooseFolder();
        }

        /// <summary>
        /// Function to select a folder on user's computer
        /// Also generates a list of filenames in the selected folder
        /// </summary>
        private void ChooseFolder()
        {
            // clear the previous search results if they exist
            if (listview_results.Items.Count > 0)
            {
                log.Log(lq.system_message, "previous results still exist - clear the results: " + listview_results.Items.Count.ToString() + " items cleared", new string[] { "selecting folder" });              
                listview_results.Items.Clear();
            }

            // select folder
            if (folder.ShowDialog() == DialogResult.OK)
            {
                log.Log(lq.system_message, "selecting folder to open", new string[] { "system" });
                // clear the box if it had values before
                list_directory_contents.Items.Clear();
                // assign the path to label
                label_actual_directory_name.Text = folder.SelectedPath;
                log.Log(lq.system_message, "opened a folder: " + folder.SelectedPath, new string[] { "system" });
                // access directory selected by the user
                filedir = new DirectoryInfo(label_actual_directory_name.Text);

                // fill the list of files in the directory (and subdirectories if the option is enabled)
                ListFilenames();             
                log.Log(lq.system_message, "listing of the files in the folder is complete",new string[] { "system" });
                // change the name of the folder select button if the folder was successfully opened and listed
                button_open_folder.Text = "Change Folder";
                // save the folder for future use
                save_last_opened_folder();
            }
        }

        /// <summary>
        /// Lists all the files in the selected folder inside a listbox
        /// </summary>
        private void ListFilenames()
        {
            // list filenames that exist in the selected directory
            try
            {
                log.Log(lq.system_message, "checking if the folder exists", new string[] { "system" });

                if (filedir.Exists)
                {
                    log.Log(lq.system_message, "found an active folder - listing files now: "+" directory option = " + subfolders_included, new string[] { "system" });
                    log.Log(lq.system_message, "[current activepath: " + filedir.FullName + "]", new string[] { "system" });
                    foreach (FileInfo fi in filedir.GetFiles("*.php", subfolders_included)) // limited to .php files as implied in the program name
                    {
                        list_directory_contents.Items.Add(fi.Name);
                    }
                    log.Log(lq.system_message, "listed files: " + list_directory_contents.Items.Count + " found",new string[] { "system" });
                }
            }
            catch (Exception e)
            {               
                System.Diagnostics.Debug.WriteLine("The process failed: {0}", e.ToString());
                    log.Log(lq.critical_message, "program exception occured: " + e.ToString(), new string[] { "program error" });
            }
            finally
            {
                log.Log(lq.system_message, "counting files in the folder " + list_directory_contents.Items.Count.ToString() + " files found", new string[] { "files" });
                statusbar_label_original_files_number.Text = "total files found: " + list_directory_contents.Items.Count;
            }
            // check files for pattern (initial pattern is empty string)
            checkFiles();
        }

#region textbox managing
        /// <summary>
        /// On text change - enable the rename button based on the information inside the textbox
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters </param>
        private void textbox_user_input_TextChanged(object sender, EventArgs e)
        {
            searchFilesForText();
        }
        /// <summary>
        /// Allows checking new filename at all times not just when text field changes from empty to filled
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters </param>
        private void textbox_user_input_KeyPress(object sender, KeyEventArgs e)
        {
            //searchFilesForText();
        }

        /// <summary>
        /// Contains the logic to check the files for user input
        /// </summary>
        private void searchFilesForText()
        {
            log.Log(lq.repeating_operation, "looking for files matching the pattern: " + textbox_user_input.Text, new string[] { "files" });

            if (listview_results.Items.Count > 0)
            {
                listview_results.Items.Clear(); // clear displayed names
                matching_files.Clear(); // clear the actual collection of files
            }
            
            checkFiles(); // fill both lists once again with new matches
            log.Log(lq.system_message, "listed file count: " + listview_results.Items.Count, new string[] { "files" });
        }
#endregion

        /// <summary>
        /// Checks each file in the directory for user input in the code, e.g insert into <table_name>
        /// </summary>
        private void checkFiles()
        {
            // reset the checkbox 
            checkBox_selectall.Checked = false;

            statusbar_label_clipboard.Text = "Clipboard status: not copied"; // refresh the clipboard label text
            bool file_match = false;

            try
            {
                if (filedir.Exists) // read files if the diretory is open
                {
                    // 1st step check all the files in currently opened directory
                    foreach (FileInfo fi in filedir.GetFiles("*.*", subfolders_included))
                    {
                        // check file extension and skip the file if it's not the right extension
                        if (fi.Extension != ".php")
                            continue;

                        // read file text into a string
                        string file_text = File.ReadAllText(fi.FullName.ToString());
                  
                        // look for a pattern in the file contents and add it to alist if necessary
                        file_match = string_contains(file_text, textbox_user_input.Text, StringComparison.OrdinalIgnoreCase); // ignore case

                        // add the file if file_match is true
                        if (file_match)
                        {
                            // add fileinfo object to a separate collection so it can later be opened by a different program
                            matching_files.Add(fi);                   

                            // add to listview for display purposes
                            // create a row 
                            ListViewItem item = new ListViewItem(fi.DirectoryName); // 1st column
                            item.SubItems.Add(fi.Name);                             // 2nd column

                            // add a row to container
                            listview_results.Items.Add(item);
                        }

                        // update ino label
                        statusbar_label_matching_files.Text = "files matching: "+matching_files.Count.ToString();
                        statusbar_label_original_files_number.Text = "total files found: " + list_directory_contents.Items.Count;
                    }
                }
            }
            catch (NullReferenceException e)
            {
                log.Log(lq.critical_message, "file listing failed: " + e.ToString(), new string[] { "system" });
            }
            catch (Exception e)
            {
                log.Log(lq.critical_message, "file listing failed - " + e.ToString(), new string[] { "system" });
            }
        }
        /// <summary>
        /// Check if one string contains another
        /// </summary>
        /// <param name="str">original string</param>
        /// <param name="substring">text to search for</param>
        /// <param name="comp">comparison option</param>
        /// <returns>true or false for pattern match</returns>
        public bool string_contains(String str, String substring, StringComparison comp)
        {
            if (substring == null)
                throw new ArgumentNullException("substring",
                                                "substring cannot be null.");
            else if (!Enum.IsDefined(typeof(StringComparison), comp))
                throw new ArgumentException("comp is not a member of StringComparison",
                                            "comp");

            return str.IndexOf(substring, comp) >= 0;
        }
        /// <summary>
        /// Function to exit the application programatically in case windows X button doesn't work or the windows bar is missing
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters </param>
        private void button_exit_program_Click(object sender, EventArgs e)
        {
            log.Log(lq.critical_message, "user clicked on the program exit button: " + e.ToString(), new string[] { "user input" });
            this.Close(); // exits the program
        }

        /// <summary>
        /// Function to clear search text from the user input textbox
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters </param>
        private void button_clear_text_search_Click(object sender, EventArgs e)
        {
            log.Log(lq.system_message, "user clicked the text input clear button", new string[] { "user input" });
            textbox_user_input.Clear(); // remove text from the textbox
        }

        /// <summary>
        /// Clicking this button will attempt to open the files containing user desired text in the default text editor on the computer
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters </param>
        private void button_open_files_Click(object sender, EventArgs e)
        {
            // prepare confirmation dialogue
            log.Log(lq.system_message, "showing a warning message - opening "+matching_files.Count+" files", new string[] { "system" });
            string message = "Open "+matching_files.Count.ToString()+" files in the associated application? Search text will be copied to clipboard for faster ctrl+f search";
            
            // execute commands
            try
            {
                if (filedir.Exists) // open files if the diretory is open
                {
                    // for all the files in the filtered list - attempt to open them
                    if (matching_files != null && matching_files.Count > 0)
                    {
                        var confirmResult = MessageBox.Show(message, "confirm action", MessageBoxButtons.YesNo);

                        if (confirmResult == DialogResult.Yes)
                        {
                            foreach (FileInfo f in matching_files)
                            {
                                //System.Diagnostics.Process.Start("notepad.exe", f.FullName); // opens files in notepad
                                System.Diagnostics.Process.Start(@f.FullName); // opens files in notepad
                                log.Log(lq.critical_message, "opened file: "+f.FullName, new string[] { "system", "user input", "files" });
                            }
                        }
                        else
                        {
                            log.Log(lq.critical_message, "user cancelled file opening of " + matching_files.Count + " files", new string[] { "system", "user input" });
                        }
                    }
                }
            }
            catch(NullReferenceException ex)
            {
                log.Log(lq.critical_message, "The file opening process failed (likely no open folder): " + ex.ToString(), new string[] { "system" });
            }
            catch(InvalidCastException ex)
            {
                log.Log(lq.critical_message, "The file opening process failed (object cast incorrect): " + ex.ToString(), new string[] { "system" });
            }
            finally
            {
                if (textbox_user_input.Text.Length > 0)
                {
                    log.Log(lq.system_message, "search pattern copied to clipboard: " + textbox_user_input.Text, new string[] { "system" });
                    Clipboard.SetText(textbox_user_input.Text); // copy user search terms into the clipboard so that they can easily use ctrl+f after files are open

                    statusbar_label_clipboard.Text = "Clipboard status: user search copied";
                }
            }
        }
        /// <summary>
        /// Checkbox status changed - for including subdirectories in search
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters </param>
        private void checkbox_include_subfolders_CheckedChanged(object sender, EventArgs e)
        {
            if(checkbox_include_subfolders.Checked)
            {
                subfolders_included = SearchOption.AllDirectories;
                log.Log(lq.system_message, "mode changed to all directories " + textbox_user_input.Text, new string[] { "system", "user input" });
            }
            else
            {
                subfolders_included = SearchOption.TopDirectoryOnly;
                log.Log(lq.system_message, "mode changed to current directory only " + textbox_user_input.Text, new string[] { "system", "user input" });
            }

            // repeat the folder opening procedure if folder exists
            // first remove all the items from existing list
            if (list_directory_contents.Items.Count > 0)
            {
                // clear the actual collection of files in the directory
                list_directory_contents.Items.Clear(); 
                // clear the actual collection of files in the post-check results list
                listview_results.Items.Clear();   
                matching_files.Clear();
            }

            ListFilenames();
        }

        /// <summary>
        /// Save the last known directory to file
        /// </summary>
        private void save_last_opened_folder()
        {
            string fdir = filedir.FullName;

            try
            {
                // serialize the directory name
                using (StreamWriter file = File.CreateText("./saved/"+"previous_directory.txt"))
                {
                    file.WriteLine(fdir);
                }
            }
            catch (DirectoryNotFoundException)
            {
                System.IO.Directory.CreateDirectory("./saved");
                using (StreamWriter file = File.CreateText("./saved/" + "previous_directory.txt"))
                {
                    file.WriteLine(fdir);
                }
            }
            catch (IOException)
            {

            }
        }

        /// <summary>
        /// Open the text file containing last known directory and assign the value
        /// </summary>
        /// <returns>Directory name</returns>
        private string get_last_opened_folder()
        {
            try
            {
                // open the file containing the previous directory name
                using (StreamReader file = File.OpenText("./saved/"+"previous_directory.txt"))
                {
                    return file.ReadLine(); // read the first line where the filename is
                }
                return "";
            }
            catch(Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// Open the last known directory
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters </param>
        private void button_open_previous_Click(object sender, EventArgs e)
        {
            // clear folder contents
            list_directory_contents.Items.Clear();
            // clear the previous search results if they exist
            if (listview_results.Items.Count > 0)
            {
                log.Log(lq.system_message, "previous results still exist - clear the results: " + listview_results.Items.Count.ToString() + " items cleared", new string[] { "selecting folder" });
                listview_results.Items.Clear();                
            }

            try
            {
                string path = @last_opened_directory;
                filedir = new DirectoryInfo(path);
                // fill the list of files in the directory (and subdirectories if the option is enabled)
                ListFilenames();
                log.Log(lq.system_message, "listing of the files in the folder is complete", new string[] { "system" });
                // change the name of the folder select button if the folder was successfully opened and listed
                button_open_folder.Text = "Change Folder";
                // do not save the directory name since it didn't change
            }
            catch (ArgumentException ex)
            {
                log.Log(lq.critical_message, "Path string is wrong: " + ex.ToString() + " [used this path: " + last_opened_directory + "]", new string[] { "system" });
            }

            log.Log(lq.critical_message, "Opened previous known folder [used this path: " + last_opened_directory + "]", new string[] { "system" });
        }
        /// <summary>
        /// Function to handle the user click on one of the list view columns. Sorts the results based on alphabet value
        /// reference: https://msdn.microsoft.com/en-us/library/ms996467.aspx
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters - in this case it contains the position of the clicked column</param>
        private void listview_results_ColumnClicked(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;
                // Set the sort order to ascending by default.
                listview_results.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listview_results.Sorting == SortOrder.Ascending)
                    listview_results.Sorting = SortOrder.Descending;
                else
                    listview_results.Sorting = SortOrder.Ascending;
            }
           
            listview_results.ListViewItemSorter = new ListViewItemComparer(e.Column, listview_results.Sorting);
            listview_results.Sort();
        }
        /// <summary>
        /// Handle the selection of all items in currently sorted list
        /// reference: https://msdn.microsoft.com/en-us/library/system.windows.forms.listviewitem.checked(v=vs.110).aspx
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters </param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_selectall.Checked == true)
            {
                for (int y = listview_results.Items.Count - 1; y >= 0; y--)
                {
                    listview_results.Items[y].Checked = true;
                }
            }
            else
            {
                for (int y = listview_results.Items.Count - 1; y >= 0; y--)
                {
                    listview_results.Items[y].Checked = false;
                }
            }
        }
        /// <summary>
        /// Open only the files that have their checkbox selected by the user
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters </param>
        private void button_open_selected_files_Click(object sender, EventArgs e)
        {
            // prepare confirmation dialogue
            log.Log(lq.system_message, "showing a warning message - opening " + number_of_checked_items + " files", new string[] { "system" });
            string message = "Open " + number_of_checked_items + " files in the associated application? Search text will be copied to clipboard for faster ctrl+f search";

            // execute commands
            try
            {
                if (filedir.Exists) // open files if the diretory is open
                {
                    // for all the files in the filtered list - attempt to open them
                    if (listview_results != null && listview_results.Items.Count > 0 && number_of_checked_items > 0)
                    {
                        var confirmResult = MessageBox.Show(message, "confirm action", MessageBoxButtons.YesNo);

                        if (confirmResult == DialogResult.Yes)
                        {
                            //foreach (FileInfo f in matching_files)
                            for(int i = 0; i < listview_results.Items.Count; i++)
                            {
                                if(listview_results.Items[i].Checked == true)
                                {
                                    string filename_to_open = listview_results.Items[i].SubItems[0].Text +"\\"+ listview_results.Items[i].SubItems[1].Text; // add folder name and file name together
                                    System.Diagnostics.Process.Start(@filename_to_open); // opens files in notepad
                                    log.Log(lq.critical_message, "opened file: " + filename_to_open, new string[] { "system", "user input", "files" });
                                }
                            }
                        }
                        else
                        {
                            log.Log(lq.critical_message, "user cancelled file opening of " + matching_files.Count + " files", new string[] { "system", "user input" });
                        }
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                log.Log(lq.critical_message, "The file opening process failed (likely no open folder): " + ex.ToString(), new string[] { "system" });
            }
            catch (InvalidCastException ex)
            {
                log.Log(lq.critical_message, "The file opening process failed (object cast incorrect): " + ex.ToString(), new string[] { "system" });
            }
            catch(InvalidOperationException ex)
            {
                log.Log(lq.critical_message, "The file opening process failed (filename nopt provided): " + ex.ToString(), new string[] { "system" });
            }
            finally
            {
                if (textbox_user_input.Text.Length > 0)
                {
                    log.Log(lq.system_message, "search pattern copied to clipboard: " + textbox_user_input.Text, new string[] { "system" });
                    Clipboard.SetText(textbox_user_input.Text); // copy user search terms into the clipboard so that they can easily use ctrl+f after files are open

                    statusbar_label_clipboard.Text = "Clipboard status: user search copied";
                }
            }
        }
        /// <summary>
        /// Get the number of currently checked items in the listview_results
        /// </summary>
        /// <returns>Number of checked items</returns>
        private int get_number_of_checked_items()
        {
            int num = 0;

            for (int i = 0; i < listview_results.Items.Count; i++)
            {
                if (listview_results.Items[i].Checked == true)
                {
                    ++num;
                }
            }

            return num;
        }
        /// <summary>
        /// Update checked item counter. This function is a delegate to ItemChecked event of this form
        /// Not to be confused with ItemCheck event (which happens prior to the checkbox status change)
        /// references:https://msdn.microsoft.com/en-us/library/system.windows.forms.itemcheckedeventhandler(v=vs.110).aspx
        /// https://msdn.microsoft.com/en-us/library/system.windows.forms.listview.itemcheck(v=vs.110).aspx
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters </param>
        private void listview_ItemCheck(object sender, System.Windows.Forms.ItemCheckedEventArgs e)
        {
            number_of_checked_items = get_number_of_checked_items();
            // update the button state and text
            if (number_of_checked_items > 0)
                this.button_open_selected_files.Enabled = true;
            else
                this.button_open_selected_files.Enabled = false;
            // update text
            this.button_open_selected_files.Text = "Open Selected Files " + number_of_checked_items.ToString();
        }
        /// <summary>
        /// Change the visual theme to light colors
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters </param>
        private void change_theme_light(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
            this.list_directory_contents.BackColor = Color.White;
            this.listview_results.BackColor = Color.White;
            this.status_strip_bottom.BackColor = Color.MistyRose;
        }
        /// <summary>
        /// Change the visual theme to dark colors
        /// </summary>
        /// <param name="sender"> reference to the object that raised the event</param>
        /// <param name="e"> event specific object with relevant parameters </param>
        private void change_theme_dark(object sender, EventArgs e)
        {
            this.BackColor = Color.DarkGray;
            this.list_directory_contents.BackColor = Color.LightGray;
            this.listview_results.BackColor = Color.LightGray;
            this.status_strip_bottom.BackColor = Color.DimGray;
        }
    }

    /// <summary>
    /// Comparer for list view items
    /// </summary>
    public class ListViewItemComparer : IComparer
    {
        private int col;
        private SortOrder order;

        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }

        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }

        public int Compare(object x, object y)
        {
            int returnVal = -1;
            returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                    ((ListViewItem)y).SubItems[col].Text);
            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            return returnVal;
        }
    }

    /// <summary>
    /// Enumeration defining two distinct theme styles for this form
    /// </summary>
    enum theme { light, dark};

}
