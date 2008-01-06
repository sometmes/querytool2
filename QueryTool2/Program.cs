using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using App;

namespace QueryTool2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoadHardCodedSettings();
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Application.Run(new QueryWindowForm());
            //Application.Run(new ChangeProviderForm());
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            My.Settings.Save();
        }

        private static void LoadHardCodedSettings()
        {
            SimpleConnectionEditPlugin.RegisterPlugin("System.Data.SqlClient", "App.SimpleConnectionUserControl.MsSqlServer");
        }

        static void LoadUserPreferences()
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            //path = Path.Combine(path, "QueryTool2\\Preferences.xml");
            ////if (File.Exists(path))
            //object ol = My.Settings.RecentConnections;
            ////RecentConnectionList l = new RecentConnectionList();
            //ConnectionInfo r = new ConnectionInfo();
            ////l.Add(r);
            //r.ConnectionString = "1";
            //r.Password = "2";
            //r.Created = DateTime.Now;
            //My.Settings.RecentConnections.Add(r);
            //My.Settings.Save();
        }
    }
}