using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using UserActivityMonitor.Services.Contracts;

namespace UserActivityMonitor.Services.Implementation
{
    public class ManageDB 
    {

        public void CreateSqlLiteDatabase()
        {
            var path = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString().Replace("Data Source=", "");
            if (!File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                string directoryFullPath = fileInfo.DirectoryName;
                System.IO.Directory.CreateDirectory(directoryFullPath);
                SQLiteConnection.CreateFile(path);
            }
            else
            {
                //string path = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString();
                //FileInfo fileInfo = new FileInfo(path);
                //string directoryFullPath = fileInfo.DirectoryName;
                //System.IO.Directory.CreateDirectory(directoryFullPath);
                //string fileName = @"C:\Temp\MaheshTXFI.txt";
                //FileInfo fi = new FileInfo(fileName);

            }

        }




        public void Changepassword()
        {
            using (SQLiteConnection con = new
           SQLiteConnection(ConfigurationManager.AppSettings["DBConnection"].ToString()))
            {
                con.Open();
                con.ChangePassword("@@#DEMOSMART#@@");
            }
        }

        public void Setpassword()
        {
            using (SQLiteConnection con = new
           SQLiteConnection(ConfigurationManager.AppSettings["DBConnection"].ToString()))
            {
                con.SetPassword("@@#DEMOSMART#@@");
            }
        }
        public void Removepassword()
        {
            using (SQLiteConnection con = new
           SQLiteConnection(ConfigurationManager.AppSettings["DBConnection"].ToString()))
            {
                con.SetPassword("@@#DEMOSMART#@@");
                con.Open();
                con.ChangePassword("");
            }
        }

        public void CreateUserActivityTable()
        {
            try
            {
                using (SQLiteConnection con = new
               SQLiteConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {

                    con.Open();
                    using (SQLiteCommand com = new SQLiteCommand(con))
                    {
                        string createTableQuery =
                        @"CREATE Table IF NOT EXISTS [UserActivity] (
                        [Id] INTEGER NOT NULL,
                        [UserName] TEXT,
	                    [ProcessName] TEXT,
                        [AppName] TEXT,
                        [StartTime] TEXT,
                        PRIMARY KEY([Id] AUTOINCREMENT)); ";
                        com.CommandText = createTableQuery;
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertUserActivity(string userName, string processName, string appName, string startTime)
        {
            try
            {
                using (SQLiteConnection con = new
               SQLiteConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString.ToString()))
                {

                    con.Open();
                    using (SQLiteCommand com = new SQLiteCommand(con))
                    {
                        string insertTableQuery = string.Format("insert into UserActivity (UserName,ProcessName,AppName,StartTime) values ('{0}','{1}','{2}','{3}');", userName, processName, appName, startTime);
                        com.CommandText = insertTableQuery;
                        com.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
