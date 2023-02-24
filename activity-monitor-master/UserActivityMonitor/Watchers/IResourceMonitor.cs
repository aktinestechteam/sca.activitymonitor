using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityMonitor.Watchers
{
    interface IResourceMonitor
    {
        /// <summary>
        /// Start the monitoring process.
        /// </summary>
        void StartMonitoring();

        /// <summary>
        /// Stops the monitoring process.
        /// </summary>
        void EndMonitoring();
    }
}
