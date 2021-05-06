using Fclp;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Retriever
{
    public static class NativeMethods
    {
        [DllImport("kernel32", SetLastError = true)]
        public static extern bool AttachConsole(int dwProcessId);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();
    }

    public class ProgramArgs
    {
        public string ConfigFile { get; set; }
        public bool RunNow { get; set; }
        public string ProfileFile { get; set; }
        public string OutFile { get; set; }
    }

    static class Program
    {
        public static bool TryAttachConsole()
        {
            try
            {
                IntPtr ptr = NativeMethods.GetForegroundWindow();
                NativeMethods.GetWindowThreadProcessId(ptr, out int u);
                Process process = Process.GetProcessById(u);
                return NativeMethods.AttachConsole(process.Id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            // Add the event handler for handling non-UI thread exceptions to the event.
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            bool consoleAttached = TryAttachConsole();
            try
            {
                AppMain(consoleAttached);
            }
            catch (Exception ex)
            {
                var debug = Environment.GetEnvironmentVariable("AWSRetriever_DEBUG");
                if (!string.IsNullOrEmpty(debug))
                {
                    Console.WriteLine(ex);
                }
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (consoleAttached)
                {
                    NativeMethods.FreeConsole();
                }
            }

        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs ex)
        {
            Console.WriteLine("Application Exception");
            Console.WriteLine(ex.Exception.Message);
            //DialogResult result = DialogResult.Cancel;
            //try
            //{
            //    result = ShowThreadExceptionDialog("Windows Forms Error", ex.Exception);
            //}
            //catch
            //{
            //    try
            //    {
            //        MessageBox.Show("Fatal Windows Forms Error",
            //            "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop);
            //    }
            //    finally
            //    {
            //        Application.Exit();
            //    }
            //}

            //// Exits the program when the user clicks Abort.
            //if (result == DialogResult.Abort)
            //    Application.Exit();
        }

        // Handle the UI exceptions by showing a dialog box, and asking the user whether
        // or not they wish to abort execution.
        // NOTE: This exception cannot be kept from terminating the application - it can only
        // log the event, and inform the user about it.
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Console.WriteLine("Application Exception");
                Exception ex = (Exception)e.ExceptionObject;
                Console.WriteLine(ex.Message);
                string errorMsg = "An application error occurred. Please contact the adminstrator " +
                    "with the following information:\n\n";

                // Since we can't prevent the app from terminating, log this to the event log.
                if (!EventLog.SourceExists("ThreadException"))
                {
                    EventLog.CreateEventSource("ThreadException", "Application");
                }

                // Create an EventLog instance and assign its source.
                EventLog myLog = new EventLog();
                myLog.Source = "ThreadException";
                myLog.WriteEntry(errorMsg + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                //try
                //{
                //    MessageBox.Show("Fatal Non-UI Error",
                //        "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                //        + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //}
                //finally
                //{
                //    Application.Exit();
                //}
            }
        }

        // Creates the error message and displays it.
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            string errorMsg = "An application error occurred. Please contact the adminstrator " +
                "with the following information:\n\n";
            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }

        private static void AppMain(bool consoleAttached)
        {
            if (consoleAttached)
            {
                Console.WriteLine("AWS Retriever");
            }
            FluentCommandLineParser<ProgramArgs> p = new FluentCommandLineParser<ProgramArgs>();
            p.Setup(arg => arg.RunNow).As('r', "run").SetDefault(false).WithDescription("Run now");
            p.Setup(arg => arg.ConfigFile).As('c', "config").SetDefault(Configuration.DefaultFileName).WithDescription("Specify configuration file");
            p.Setup(arg => arg.ProfileFile).As('p', "profile").WithDescription("Profile to run");
            p.Setup(arg => arg.OutFile).As('o', "output").WithDescription("Output file").SetDefault("retriver.objects.json");
            p.SetupHelp("h", "help").Callback(text =>
                {
                    Console.WriteLine("Usage:");
                    Console.WriteLine(text);
                });
            var result = p.Parse(Environment.GetCommandLineArgs());
            if (result.HasErrors)
            {
                Console.WriteLine(result.ErrorText);
                return;
            }
            if (result.HelpCalled)
            {
                return;
            }
            Configuration.Load(p.Object.ConfigFile);
            if (p.Object.RunNow)
            {
                ConsoleScanner cs = new ConsoleScanner();
                string profile = p.Object.ProfileFile;
                if (string.IsNullOrEmpty(profile))
                {
                    profile = Configuration.Instance.Profile;
                }
                cs.Scan(p.Object.OutFile,profile);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
                Configuration.Instance.Save();
            }

        }

    }
}
