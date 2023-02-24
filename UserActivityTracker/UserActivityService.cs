using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using UserActivityTracker.Implementation;
using UserActivityTracker.Watchers;

namespace UserActivityTracker
{
    public partial class UserActivityService : ServiceBase
    {
        System.Timers.Timer timeDelay;
        int count;
        private ManageDB mdb = null;
        private string userName = "";
        private List<String> runningProgramList = new List<String>();
        private List<Process> procList;

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hwnd, StringBuilder ss, int count);

        public UserActivityService()
        {
            InitializeComponent();
            timeDelay = new System.Timers.Timer();
            timeDelay.Elapsed += new System.Timers.ElapsedEventHandler(WorkProcess);
        }
        public void WorkProcess(object sender, System.Timers.ElapsedEventArgs e)
        {
            string process = "Timer Tick " + userName;
            LogService(process);
            count++;
        }
        protected override void OnStart(string[] args)
        {
            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            mdb = new ManageDB();
            mdb.CreateUserActivityTable();
            procList = new List<Process>();
            LogService("Service is Started");
            timeDelay.Interval = 1000;
            timeDelay.Enabled = true;
        }
        protected override void OnStop()
        {
            LogService("Service Stoped");
            timeDelay.Enabled = false;
        }
        private void LogService(string content)
        {
            //mdb.InsertUserActivity("1", "2", "3", "4");
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
                if (runningProgramList.IndexOf(appName) > -1 && (runningProgramList.LastIndexOf(appName) < runningProgramList.Count - 1))
                {
                    runningProgramList.Add(appName);
                    startTime = DateTime.Now.ToString("dd MMM yyy hh:mm:ss tt");
                    //Console.WriteLine(String.Format("{0}\t - {1}\t - {2}", processName, appName, startTime));
                    mdb.InsertUserActivity(userName, processName, appName, startTime);
                }
                else if (runningProgramList.IndexOf(appName) < 0)
                {
                    runningProgramList.Add(appName);
                    startTime = DateTime.Now.ToString("dd MMM yyy hh:mm:ss tt");
                    //Console.WriteLine(String.Format("{0}\t - {1}\t - {2}", processName, appName, startTime));
                    mdb.InsertUserActivity(userName, processName, appName, startTime);
                }
            }




            //FileStream fs = new FileStream(@"c:\TestServiceLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            //StreamWriter sw = new StreamWriter(fs);
            //sw.BaseStream.Seek(0, SeekOrigin.End);
            //sw.WriteLine(content);
            //sw.Flush();
            //sw.Close();
        }
    }
}
