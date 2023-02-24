using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using AppliationStatusConsole;

namespace timespend
{
    class Program
    {
        static void Main(string[] args)
        {
            ActiveApplicationPropertyThread activeWorker = new ActiveApplicationPropertyThread();
            Thread activeThread = new Thread(activeWorker.StartThread);
            activeThread.Start();
            Dictionary<string, Double> temp;
            while (true)
            {
                Console.WriteLine(activeWorker.AppInfo);
                if (activeWorker.activeApplicationInfomationCollector._focusedApplication.Keys.Count > 5)
                {
                    temp = activeWorker.activeApplicationInfomationCollector._focusedApplication;

                    break;
                }

                Thread.Sleep(1000);
            }

            foreach (var j in temp.Keys)
            {
                Console.WriteLine(j + "         " + temp[j] / 1000);

            }
        }
    }
}
