using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;
using Polling.Network;
using ActivityMonitoring;
using Polling.Services.Implementation;

namespace Polling
{
    /// <summary>
    /// Class for watching the opening of programs.
    /// </summary>
    public class ProgramWatcher : IResourceMonitor
    {
        private List<String> runningProgramList = new List<String>();
        private List<Process> procList;
        private Timer timer = null;
        private static ProgramWatcher instance = new ProgramWatcher();
        private ManageDB mdb = null;
        private string userName = "";

       [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hwnd, StringBuilder ss, int count);

        /// <summary>
        /// Constructor
        /// </summary>
        private ProgramWatcher()
        {
            // do nothing
        }

        /// <summary>
        /// Singleton implementation.
        /// </summary>
        /// <returns>instance</returns>
        public static ProgramWatcher GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// Helper callback method for use of the timer object in order to poll.
        /// </summary>
        /// <param name="temp">needed argument</param>
        private void WatchProgram(object temp)
        {
            Console.WriteLine("Start");
            //Check new process opening
            foreach (Process P in Process.GetProcesses())
            {
         
                if (P.MainWindowTitle.Length > 0)
                {
                    try
                    {
                        if (!runningProgramList.Contains(P.ProcessName+ P.MainWindowTitle))
                        {
                            try
                            {
                                runningProgramList.Add(P.ProcessName+ P.MainWindowTitle);
                                procList.Add(P);
                            }
                            catch (Exception e)
                            {
                                
                            }//swallow

                            String message = ResourceIdentifiers.PROGRAM_IDENTIFIER + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                             + P.ProcessName + Constants.SPACE + Constants.SPLITTER + Constants.SPACE
                                             + P.MainWindowTitle + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                             + P.ToString() + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                             + P.MainModule.FileName + Constants.SPACE + Constants.SPLITTER + Constants.SPACE 
                                             + DateTime.Now;
                            EventSender.GetInstance().ProcessMessage(message);
                                                           
                           
                        }
                    }
                    catch (Exception e)
                    {
                       
                    }//swallow
                   
                }
            }
                  
           
        }

        private void GetActiveWindow(object temp)
        {
            //Create the variable
            const int nChar = 256;
            StringBuilder ss = new StringBuilder(nChar);
            //Run GetForeGroundWindows and get active window informations
            //assign them into handle pointer variable
            IntPtr handle = IntPtr.Zero;
            handle = GetForegroundWindow();
            string appName = "";
            string processName = "";

            string startTime = "";

           

            if (GetWindowText(handle, ss, nChar) > 0)
            {

                uint pid;
                GetWindowThreadProcessId(handle, out pid);
                Process p = Process.GetProcessById((int)pid);
                processName = p.ProcessName;

                appName = ss.ToString();
                if (appName == "Task Switching")
                    return;
                if (runningProgramList.IndexOf(appName) >-1 && (runningProgramList.LastIndexOf(appName)< runningProgramList.Count-1))
                {
                    runningProgramList.Add(appName);
                    startTime = DateTime.Now.ToString("dd MMM yyy hh:mm:ss tt");
                    Console.WriteLine(String.Format("{0}\t - {1}\t - {2}", processName, appName,startTime));
                    mdb.InsertUserActivity(userName, processName, appName, startTime);
                }
                else if (runningProgramList.IndexOf(appName) < 0)
                {
                    runningProgramList.Add(appName);
                    startTime = DateTime.Now.ToString("dd MMM yyy hh:mm:ss tt");
                    Console.WriteLine(String.Format("{0}\t - {1}\t - {2}", processName, appName, startTime));
                    mdb.InsertUserActivity(userName, processName, appName, startTime);

                }

            }

            
        }

        #region IResourceMonitor Members

        public void StartMonitoring()
        {
            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            mdb = new ManageDB();
            mdb.CreateUserActivityTable();
            procList = new List<Process>();
            TimerCallback timerDelegate = new TimerCallback(GetActiveWindow);
            timer = new Timer(timerDelegate, null, 0, 1000);
            timerDelegate.Invoke(new object());
        }

        public void EndMonitoring()
        {
            timer = null;
            runningProgramList = null;
            procList = null;
        }


        private void WatchApplicationIdealEvent()
        {
            MouseHook.Start();
            MouseHook.MouseAction += new EventHandler(Event);

        }
        private void Event(object sender, EventArgs e) { Console.WriteLine("Left mouse click!"); }

        #endregion


    }
}
