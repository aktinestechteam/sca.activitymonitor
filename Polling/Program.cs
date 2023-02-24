using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Polling.Network;
using Polling.Services.Implementation;

namespace Polling
{
    /// <summary>
    /// Main class.
    /// </summary>
    public class Program
    {
        
        public static Boolean active = true;

        /// <summary>
        /// Entry method.
        /// </summary>
        /// <param name="args">arguments</param>
        public static void Main(string[] args)
        {
            //COMReceiver.GetInstance().DeleteAllMessages();

            //EventSender.GetInstance().StartConnection(ServerInformation.LOCAL_HOST, ServerInformation.DEFAULT_PORT);

            /* Get input from COM */
            //COMReceiver.GetInstance().StartReceivingMessages();

            /* Get WebPage URL from IE */
            WebPageWatcher.GetInstance().StartMonitoring();

            /* Monitors files */
            //GeneralFileWatcher.GetInstance().Start();
            //FileMonitor.GetInstance().StartMonitoring();

            /* Monitors Apps/Programs */
            ProgramWatcher.GetInstance().StartMonitoring();

            //EventLog log = new EventLog("Security");
            //var entries = log.Entries.Cast<EventLogEntry>()
            //                         .Where(x => x.EventID == 4624)
            //                         .Select(x => new
            //                         {
            //                             x.MachineName,
            //                             x.Site,
            //                             x.Source,
            //                             x.Message,
            //                             x.UserName,
            //                             x.TimeGenerated,
            //                             x.TimeWritten,
            //                             x.Category,
            //                             x.EventID

            //                         }).ToList();

            //foreach(var e in entries)
            //{
            //    Console.WriteLine(string.Format("EventId:{5},Category :{0}/tMessage:1{1}/tTimeGenerated:{2}/tTimeWritten:{3}/tUserName:{4}",e.Category,"",e.TimeGenerated,e.TimeWritten,e.UserName,e.EventID));
            //}
            //Console.WriteLine(entries.Where(x => Convert.ToDateTime(x.TimeWritten) > new DateTime(2021, 11, 23)).Min(x =>Convert.ToDateTime(x.TimeWritten)));



            /* Monitors Folder/Directories */
            //FolderMonitor.GetInstance().StartMonitoring();

            // Monitor emails
            //EmailMonitor.GetInstance().StartMonitoring();

            while (active)
            {
                // runs indefinitely until terminated by user via task manager
            }
        }
    }
}
