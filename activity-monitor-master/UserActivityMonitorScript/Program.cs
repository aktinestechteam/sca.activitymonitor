using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserActivityMonitorScript
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Aktines\Data\SmartData.db3";
            if (!File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                string directoryFullPath = fileInfo.DirectoryName;
                System.IO.Directory.CreateDirectory(directoryFullPath);
                using (File.Create(path)) { } ;
            }
        }
    }
}
