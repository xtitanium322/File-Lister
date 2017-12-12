using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json.Linq;        // the Json serializer 
using Newtonsoft.Json; // serializer

namespace Php_File_Lister
{
    /// <summary>
    /// Log Qualifier = message category
    /// By specifying the minimum qualifier level user effectively turns off the lower level logging, making a smaller log file and only getting the necessary information back
    /// system message - general information on what the system is currently doing
    /// critical message - information that must always be logged
    /// value tracking - information about some value thatshould be tracked
    /// </summary>
    public enum lq { system_message = 1, critical_message, repeating_operation }; // ordered by importance (low to high)

    /// <summary>
    /// A class for storing log message information and transforming it to a Json string, that can later be read and organized by a dedicated web app
    /// </summary>
    public class LogMessage
    {
        public string timestamp { get; set; }                   // regular time of day
        public long millisecond { get; set; }                   // program run millisecond
        public string qualifier { get; set; }                   // category of  the message
        public string caller { get; set; }                      // function executed
        public string message { get; set; }                     // a message about the process, can include variables and other details
        public string[] tags;                                   // tag this log message with any unique tag - to assist filtering
    }

    /// <summary>
    /// A logging singleton class that is responsible for creating metadata about the program and assist with documentation/debugging and error deduction.
    /// Contains multiple levels of filtering to only show the necessary data.
    /// Should include the local time, millisecond and frame number (for this MonoGame project)
    /// 
    /// </summary>
    public class Logger
    {
        private static Logger _instance;          // ensure there is only one instance
        private string _log_filename_base;        // file to which a log should be saved, contains the base name will be suffixed by current time
        private string daytime;                   // contains current daytime in a string format
        private lq[] show;                        // assign all the logging qualifiers that should be shown
        private List<LogMessage> message_archive; // all log messages

        public static Logger get_instance(lq[] minimum)
        {
            if (_instance == null)
            {
                _instance = new Logger(minimum);
            }
            return _instance;
        }
        /// <summary>
        /// Construct the logger
        /// </summary>
        /// <param name="minimum">minimum visible level</param>
        private Logger(lq[] minimum)
        {
            daytime = DateTime.Now.Hour.ToString("D2") + "-" + DateTime.Now.Minute.ToString("D2") + "-" + DateTime.Now.Second.ToString("D2") +"."+DateTime.Now.Millisecond.ToString("D3")+ DateTime.Now.ToString("tt", System.Globalization.CultureInfo.InvariantCulture);
            _log_filename_base = "./logs/" + "execution_log_" + daytime + ".txt";
            show = minimum;
            message_archive = new List<LogMessage>();
        }







        /// <summary>
        /// Get the calling function to assist tracking
        /// </summary>
        /// <returns>function and class names</returns>
        private string get_caller()
        {
            string caller = "trace: ";

            StackTrace stackTrace = new System.Diagnostics.StackTrace();

            for (int i = 2; i < 4; i++) // skip 0 since it is the get_caller() function itself, skip 1 because it is the Log() function
            {
                try
                {
                    StackFrame frame = stackTrace.GetFrames()[i];
                    MethodInfo method = (MethodInfo)frame.GetMethod();
                    string methodName = method.Name;
                    //Type methodsClass = method.DeclaringType;
                    caller += ( i == 2?"" : " | " ) + methodName; // if the first element of the search do not add a pipe character
                }

                catch(InvalidCastException)
                {
                    // not a method on stack
                }
            }

            return caller;
        }
        /// <summary>
        /// This function builds and writes a JSON formatted log message into the text file
        /// This text file can later be imported either into a JSON reader program/web app or a dedicated web app build specifically for your project
        /// </summary>
        /// <param name="q">qualifier - used to organize the messages</param>
        /// <param name="m">actual message</param>
        /// <param name="caller_function">function that contains the logger call</param>
        public void Log(lq q, string m, string[] tags_array = null)
        {
            // omit all irrelevant logging messages
            if (!show.Contains(q))
                return;
            // calculate time,millisecond,frame         
            string daytime = DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2") + ":" + DateTime.Now.Second.ToString("D2") +"."+DateTime.Now.Millisecond.ToString("D3") + " " + DateTime.Now.ToString("tt", System.Globalization.CultureInfo.InvariantCulture);
            // add process tag
            // create and write a message
            try
            {
                // create a temporary json object with current message
                LogMessage temp = new LogMessage
                {
                    timestamp = daytime,
                    qualifier = q.ToString(),
                    caller = _instance.get_caller(),
                    message = m,
                    tags = tags_array
                };
                // add message to the archive 
                message_archive.Add(temp);

                // serialize the entire archive, overwriting the file
                using (StreamWriter file = File.CreateText(_instance._log_filename_base))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    // serialize object directly into file stream
                    serializer.Serialize(file, message_archive);
                }
            }
            catch (DirectoryNotFoundException e)
            {
                System.IO.Directory.CreateDirectory("./logs");
                using (StreamWriter stream = File.AppendText(_instance._log_filename_base))
                {
                    stream.Write("created a logs directory due to: " + e.ToString());
                }
            }
            catch (IOException e)
            {
                Debug.WriteLine("could not write to a log: " + e);
            }
        }
    }
}
