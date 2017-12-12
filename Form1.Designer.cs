namespace Php_File_Lister
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label_directory_name = new System.Windows.Forms.Label();
            this.list_directory_contents = new System.Windows.Forms.ListBox();
            this.label_actual_directory_name = new System.Windows.Forms.Label();
            this.textbox_user_input = new System.Windows.Forms.TextBox();
            this.label_search_for = new System.Windows.Forms.Label();
            this.button_open_folder = new System.Windows.Forms.Button();
            this.button_exit_program = new System.Windows.Forms.Button();
            this.button_clear_text_search = new System.Windows.Forms.Button();
            this.button_open_files = new System.Windows.Forms.Button();
            this.status_strip_bottom = new System.Windows.Forms.StatusStrip();
            this.statusbar_label_clipboard = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusbar_label_original_files_number = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusbar_label_matching_files = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkbox_include_subfolders = new System.Windows.Forms.CheckBox();
            this.listview_results = new System.Windows.Forms.ListView();
            this.header_folder = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.header_filename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_open_previous = new System.Windows.Forms.Button();
            this.label_previous_folder_name = new System.Windows.Forms.Label();
            this.label_description1 = new System.Windows.Forms.Label();
            this.checkBox_selectall = new System.Windows.Forms.CheckBox();
            this.button_open_selected_files = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lightThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.status_strip_bottom.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_directory_name
            // 
            this.label_directory_name.AutoSize = true;
            this.label_directory_name.Location = new System.Drawing.Point(23, 33);
            this.label_directory_name.Name = "label_directory_name";
            this.label_directory_name.Size = new System.Drawing.Size(79, 13);
            this.label_directory_name.TabIndex = 0;
            this.label_directory_name.Text = "directory name:";
            // 
            // list_directory_contents
            // 
            this.list_directory_contents.BackColor = System.Drawing.Color.White;
            this.list_directory_contents.FormattingEnabled = true;
            this.list_directory_contents.Location = new System.Drawing.Point(12, 151);
            this.list_directory_contents.Name = "list_directory_contents";
            this.list_directory_contents.Size = new System.Drawing.Size(391, 420);
            this.list_directory_contents.TabIndex = 1;
            // 
            // label_actual_directory_name
            // 
            this.label_actual_directory_name.AutoSize = true;
            this.label_actual_directory_name.BackColor = System.Drawing.Color.YellowGreen;
            this.label_actual_directory_name.Location = new System.Drawing.Point(108, 33);
            this.label_actual_directory_name.Name = "label_actual_directory_name";
            this.label_actual_directory_name.Size = new System.Drawing.Size(57, 13);
            this.label_actual_directory_name.TabIndex = 3;
            this.label_actual_directory_name.Text = "folder path";
            // 
            // textbox_user_input
            // 
            this.textbox_user_input.BackColor = System.Drawing.Color.SeaShell;
            this.textbox_user_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textbox_user_input.Location = new System.Drawing.Point(108, 52);
            this.textbox_user_input.Name = "textbox_user_input";
            this.textbox_user_input.Size = new System.Drawing.Size(731, 20);
            this.textbox_user_input.TabIndex = 4;
            this.textbox_user_input.TextChanged += new System.EventHandler(this.textbox_user_input_TextChanged);
            this.textbox_user_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_user_input_KeyPress);
            // 
            // label_search_for
            // 
            this.label_search_for.AutoSize = true;
            this.label_search_for.Location = new System.Drawing.Point(13, 54);
            this.label_search_for.Name = "label_search_for";
            this.label_search_for.Size = new System.Drawing.Size(89, 13);
            this.label_search_for.TabIndex = 5;
            this.label_search_for.Text = "text to search for:";
            // 
            // button_open_folder
            // 
            this.button_open_folder.BackColor = System.Drawing.Color.YellowGreen;
            this.button_open_folder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_open_folder.Location = new System.Drawing.Point(108, 78);
            this.button_open_folder.Name = "button_open_folder";
            this.button_open_folder.Size = new System.Drawing.Size(131, 23);
            this.button_open_folder.TabIndex = 6;
            this.button_open_folder.Text = "Open Folder";
            this.button_open_folder.UseVisualStyleBackColor = false;
            this.button_open_folder.Click += new System.EventHandler(this.button_open_folder_Click);
            // 
            // button_exit_program
            // 
            this.button_exit_program.BackColor = System.Drawing.Color.Coral;
            this.button_exit_program.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_exit_program.Location = new System.Drawing.Point(882, 577);
            this.button_exit_program.Name = "button_exit_program";
            this.button_exit_program.Size = new System.Drawing.Size(106, 26);
            this.button_exit_program.TabIndex = 7;
            this.button_exit_program.Text = "Exit";
            this.button_exit_program.UseVisualStyleBackColor = false;
            this.button_exit_program.Click += new System.EventHandler(this.button_exit_program_Click);
            // 
            // button_clear_text_search
            // 
            this.button_clear_text_search.BackColor = System.Drawing.Color.YellowGreen;
            this.button_clear_text_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_clear_text_search.Location = new System.Drawing.Point(845, 49);
            this.button_clear_text_search.Name = "button_clear_text_search";
            this.button_clear_text_search.Size = new System.Drawing.Size(134, 23);
            this.button_clear_text_search.TabIndex = 11;
            this.button_clear_text_search.Text = "Clear Text Search";
            this.button_clear_text_search.UseVisualStyleBackColor = false;
            this.button_clear_text_search.Click += new System.EventHandler(this.button_clear_text_search_Click);
            // 
            // button_open_files
            // 
            this.button_open_files.BackColor = System.Drawing.Color.YellowGreen;
            this.button_open_files.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_open_files.Location = new System.Drawing.Point(413, 577);
            this.button_open_files.Name = "button_open_files";
            this.button_open_files.Size = new System.Drawing.Size(139, 26);
            this.button_open_files.TabIndex = 14;
            this.button_open_files.Text = "Open All Matched Files";
            this.button_open_files.UseVisualStyleBackColor = false;
            this.button_open_files.Click += new System.EventHandler(this.button_open_files_Click);
            // 
            // status_strip_bottom
            // 
            this.status_strip_bottom.BackColor = System.Drawing.Color.MistyRose;
            this.status_strip_bottom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.status_strip_bottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusbar_label_clipboard,
            this.statusbar_label_original_files_number,
            this.statusbar_label_matching_files});
            this.status_strip_bottom.Location = new System.Drawing.Point(0, 609);
            this.status_strip_bottom.Name = "status_strip_bottom";
            this.status_strip_bottom.Size = new System.Drawing.Size(1000, 24);
            this.status_strip_bottom.SizingGrip = false;
            this.status_strip_bottom.TabIndex = 16;
            this.status_strip_bottom.Text = "Status:";
            // 
            // statusbar_label_clipboard
            // 
            this.statusbar_label_clipboard.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusbar_label_clipboard.Name = "statusbar_label_clipboard";
            this.statusbar_label_clipboard.Size = new System.Drawing.Size(156, 19);
            this.statusbar_label_clipboard.Text = "Clipboard status: not copied";
            // 
            // statusbar_label_original_files_number
            // 
            this.statusbar_label_original_files_number.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusbar_label_original_files_number.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusbar_label_original_files_number.ForeColor = System.Drawing.Color.OliveDrab;
            this.statusbar_label_original_files_number.Name = "statusbar_label_original_files_number";
            this.statusbar_label_original_files_number.Size = new System.Drawing.Size(106, 19);
            this.statusbar_label_original_files_number.Text = "total files found: 0";
            // 
            // statusbar_label_matching_files
            // 
            this.statusbar_label_matching_files.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusbar_label_matching_files.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusbar_label_matching_files.ForeColor = System.Drawing.Color.YellowGreen;
            this.statusbar_label_matching_files.Name = "statusbar_label_matching_files";
            this.statusbar_label_matching_files.Size = new System.Drawing.Size(98, 19);
            this.statusbar_label_matching_files.Text = "files matching: 0";
            // 
            // checkbox_include_subfolders
            // 
            this.checkbox_include_subfolders.AutoSize = true;
            this.checkbox_include_subfolders.Location = new System.Drawing.Point(108, 107);
            this.checkbox_include_subfolders.Name = "checkbox_include_subfolders";
            this.checkbox_include_subfolders.Size = new System.Drawing.Size(157, 17);
            this.checkbox_include_subfolders.TabIndex = 17;
            this.checkbox_include_subfolders.Text = "include subfolders in search";
            this.checkbox_include_subfolders.UseVisualStyleBackColor = true;
            this.checkbox_include_subfolders.CheckedChanged += new System.EventHandler(this.checkbox_include_subfolders_CheckedChanged);
            // 
            // listview_results
            // 
            this.listview_results.CheckBoxes = true;
            this.listview_results.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.header_folder,
            this.header_filename});
            this.listview_results.FullRowSelect = true;
            this.listview_results.Location = new System.Drawing.Point(413, 150);
            this.listview_results.Name = "listview_results";
            this.listview_results.Size = new System.Drawing.Size(575, 421);
            this.listview_results.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listview_results.TabIndex = 18;
            this.listview_results.UseCompatibleStateImageBehavior = false;
            this.listview_results.View = System.Windows.Forms.View.Details;
            this.listview_results.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listview_results_ColumnClicked);
            this.listview_results.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listview_ItemCheck);
            // 
            // header_folder
            // 
            this.header_folder.Text = "folder name";
            this.header_folder.Width = 250;
            // 
            // header_filename
            // 
            this.header_filename.Text = "filename";
            this.header_filename.Width = 220;
            // 
            // button_open_previous
            // 
            this.button_open_previous.BackColor = System.Drawing.Color.YellowGreen;
            this.button_open_previous.Enabled = false;
            this.button_open_previous.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_open_previous.Location = new System.Drawing.Point(245, 78);
            this.button_open_previous.Name = "button_open_previous";
            this.button_open_previous.Size = new System.Drawing.Size(131, 23);
            this.button_open_previous.TabIndex = 19;
            this.button_open_previous.Text = "Open Previous Folder";
            this.button_open_previous.UseVisualStyleBackColor = false;
            this.button_open_previous.Click += new System.EventHandler(this.button_open_previous_Click);
            // 
            // label_previous_folder_name
            // 
            this.label_previous_folder_name.AutoSize = true;
            this.label_previous_folder_name.Location = new System.Drawing.Point(194, 127);
            this.label_previous_folder_name.Name = "label_previous_folder_name";
            this.label_previous_folder_name.Size = new System.Drawing.Size(139, 13);
            this.label_previous_folder_name.TabIndex = 20;
            this.label_previous_folder_name.Text = "label_previous_folder_name";
            // 
            // label_description1
            // 
            this.label_description1.AutoSize = true;
            this.label_description1.Location = new System.Drawing.Point(105, 128);
            this.label_description1.Name = "label_description1";
            this.label_description1.Size = new System.Drawing.Size(83, 13);
            this.label_description1.TabIndex = 21;
            this.label_description1.Text = "Previous Folder:";
            // 
            // checkBox_selectall
            // 
            this.checkBox_selectall.AutoSize = true;
            this.checkBox_selectall.Location = new System.Drawing.Point(418, 127);
            this.checkBox_selectall.Name = "checkBox_selectall";
            this.checkBox_selectall.Size = new System.Drawing.Size(67, 17);
            this.checkBox_selectall.TabIndex = 22;
            this.checkBox_selectall.Text = "select all";
            this.checkBox_selectall.UseVisualStyleBackColor = true;
            this.checkBox_selectall.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button_open_selected_files
            // 
            this.button_open_selected_files.BackColor = System.Drawing.Color.YellowGreen;
            this.button_open_selected_files.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_open_selected_files.Location = new System.Drawing.Point(558, 577);
            this.button_open_selected_files.Name = "button_open_selected_files";
            this.button_open_selected_files.Size = new System.Drawing.Size(139, 26);
            this.button_open_selected_files.TabIndex = 23;
            this.button_open_selected_files.Text = "Open Selected Files";
            this.button_open_selected_files.UseVisualStyleBackColor = false;
            this.button_open_selected_files.Click += new System.EventHandler(this.button_open_selected_files_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DimGray;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightThemeToolStripMenuItem,
            this.darkThemeToolStripMenuItem});
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.Black;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 20);
            this.toolStripMenuItem1.Text = "Change Theme";
            this.toolStripMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // lightThemeToolStripMenuItem
            // 
            this.lightThemeToolStripMenuItem.BackColor = System.Drawing.Color.Gray;
            this.lightThemeToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.lightThemeToolStripMenuItem.Name = "lightThemeToolStripMenuItem";
            this.lightThemeToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.lightThemeToolStripMenuItem.Text = "light theme";
            this.lightThemeToolStripMenuItem.Click += new System.EventHandler(this.change_theme_light);
            // 
            // darkThemeToolStripMenuItem
            // 
            this.darkThemeToolStripMenuItem.BackColor = System.Drawing.Color.Gray;
            this.darkThemeToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.darkThemeToolStripMenuItem.Name = "darkThemeToolStripMenuItem";
            this.darkThemeToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.darkThemeToolStripMenuItem.Text = "dark theme";
            this.darkThemeToolStripMenuItem.Click += new System.EventHandler(this.change_theme_dark);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 633);
            this.Controls.Add(this.button_open_selected_files);
            this.Controls.Add(this.checkBox_selectall);
            this.Controls.Add(this.label_description1);
            this.Controls.Add(this.label_previous_folder_name);
            this.Controls.Add(this.button_open_previous);
            this.Controls.Add(this.listview_results);
            this.Controls.Add(this.checkbox_include_subfolders);
            this.Controls.Add(this.status_strip_bottom);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button_open_files);
            this.Controls.Add(this.button_clear_text_search);
            this.Controls.Add(this.button_exit_program);
            this.Controls.Add(this.button_open_folder);
            this.Controls.Add(this.label_search_for);
            this.Controls.Add(this.textbox_user_input);
            this.Controls.Add(this.label_actual_directory_name);
            this.Controls.Add(this.list_directory_contents);
            this.Controls.Add(this.label_directory_name);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "File Checker";
            this.status_strip_bottom.ResumeLayout(false);
            this.status_strip_bottom.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_directory_name;
        private System.Windows.Forms.ListBox list_directory_contents;
        private System.Windows.Forms.Label label_actual_directory_name;
        private System.Windows.Forms.TextBox textbox_user_input;
        private System.Windows.Forms.Label label_search_for;
        private System.Windows.Forms.Button button_open_folder;
        private System.Windows.Forms.Button button_exit_program;
        private System.Windows.Forms.Button button_clear_text_search;
        private System.Windows.Forms.Button button_open_files;
        private System.Windows.Forms.StatusStrip status_strip_bottom;
        private System.Windows.Forms.ToolStripStatusLabel statusbar_label_clipboard;
        private System.Windows.Forms.ToolStripStatusLabel statusbar_label_matching_files;
        private System.Windows.Forms.CheckBox checkbox_include_subfolders;
        private System.Windows.Forms.ToolStripStatusLabel statusbar_label_original_files_number;
        private System.Windows.Forms.ListView listview_results;
        private System.Windows.Forms.ColumnHeader header_filename;
        private System.Windows.Forms.ColumnHeader header_folder;
        private System.Windows.Forms.Button button_open_previous;
        private System.Windows.Forms.Label label_previous_folder_name;
        private System.Windows.Forms.Label label_description1;
        private System.Windows.Forms.CheckBox checkBox_selectall;
        private System.Windows.Forms.Button button_open_selected_files;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem lightThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkThemeToolStripMenuItem;
    }
}

