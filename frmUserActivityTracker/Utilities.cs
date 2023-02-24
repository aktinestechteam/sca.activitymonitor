using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frmUserActivityTracker
{
    public static class Utilities
    {


        public static string ConvertSecondsToDateTime(double? seconds)
        {
            var sec = Convert.ToInt32(seconds);
            int hour = sec / 3600;
            int min = (sec % 3600) / 60;
            sec = sec % 60;
            return string.Format("{0:D2}:{1:D2}:{2:D2}",hour,min,sec);
        }


    }
}
