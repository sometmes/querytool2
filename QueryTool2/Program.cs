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
            LoadUserPreferences();
            Application.Run(new QueryWindowForm());
            //Application.Run(new ChangeProviderForm());
        }

        static void LoadUserPreferences()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path = Path.Combine(path, "QueryTool2\\Preferences.xml");
            //if (File.Exists(path))
            object ol = App.Settings.RecentConnections;
            App.Settings.kkvaca = "prova2";
            //RecentConnectionList l = new RecentConnectionList();
            RecentConnection r = new RecentConnection();
            //l.Add(r);
            r.ConnectionString = "1";
            r.Password = "2";
            r.Created = DateTime.Now;
            App.Settings.RecentConnections.Add(r);
            App.Settings.Save();

        }
    }

    public class App
    {
        internal static QueryTool2.Properties.Settings Settings
        {
            get
            {
                return QueryTool2.Properties.Settings.Default;
            }
        }
    }
}