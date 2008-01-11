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
            //LoadHardCodedSettings();
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

            //My.Settings.RecentConnections.ProviderInvariantName = "puta merda dels collons!!!";
            //My.Settings.RecentConnections.Connections.Add(new ConnectionInfo());

            My.Settings.RecentConnections.Clear();
            My.Settings.RecentConnections.Add(new ConnectionInfoList("uno"));
            My.Settings.RecentConnections[0].Connections.Add(new ConnectionInfo());
            My.Settings.RecentConnections[0].Connections[0].ConnectionString = "AAAAAAAAAAAAAAAAAAAAA";
            My.Settings.RecentConnections[0].Connections.Add(new ConnectionInfo());
            My.Settings.RecentConnections[0].Connections[1].ConnectionString = "BBBBBBBBBBBBBBBBBBBBB";
            My.Settings.RecentConnections[0].Connections.Add(new ConnectionInfo());
            My.Settings.RecentConnections[0].Connections[2].ConnectionString = "CCCCCCCCCCCCCCCCCCCCC";
            My.Settings.RecentConnections.Add(new ConnectionInfoList("dos"));
            My.Settings.RecentConnections[1].Connections.Add(new ConnectionInfo());
            My.Settings.RecentConnections[1].Connections[0].ConnectionString = "2222222222222222222222";
            My.Settings.RecentConnections.Add(new ConnectionInfoList("tres"));
            My.Settings.RecentConnections[2].Connections.Add(new ConnectionInfo());
            My.Settings.RecentConnections[2].Connections[0].ConnectionString = "3333333333333333333333";

            //My.Settings.RecentConnections.Add("kkdelavaca");


            //System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(My.Settings.RecentConnections.GetType());
            //TextWriter t =
            My.Settings.Save();
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