using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SqlClient;
using QueryTool2.Plugins;

namespace QueryTool2.MsSqlPlugIn
{
    public partial class SimpleConnectionEditor : UserControl, ISimpleConnectionEdit
    {
        DbProviderFactory _factory;
        IConnectionInfo _connectionInfo;
        SqlConnectionStringBuilder _cnBuilder;

        public SimpleConnectionEditor()
        {
            InitializeComponent();
        }

        private void BindControls()
        {
            Server.Text = _cnBuilder.DataSource;
            Database.Text = _cnBuilder.InitialCatalog;
            Login.Text = _cnBuilder.UserID;
            Pwd.Text = _connectionInfo.Password;
        }

        public void EditConnection(DbProviderFactory factory, IConnectionInfo cninfo)
        {
            _factory = factory;
            _connectionInfo = cninfo;
            _cnBuilder = _factory.CreateConnectionStringBuilder() as SqlConnectionStringBuilder;
            _cnBuilder.ConnectionString = _connectionInfo.ConnectionString;
            BindControls();
        }

        private void MsSqlServer_Load(object sender, EventArgs e)
        {
            Server.Validated+=new EventHandler(Server_Validated);
            Database.Validated += new EventHandler(Database_Validated);
            Login.Validated += new EventHandler(Login_Validated);
            Pwd.Validated += new EventHandler(Pwd_Validated);
        }

        void Pwd_Validated(object sender, EventArgs e)
        {
            _connectionInfo.Password = Pwd.Text;
        }

        void Login_Validated(object sender, EventArgs e)
        {
            _cnBuilder.UserID = Login.Text;
        }

        void Database_Validated(object sender, EventArgs e)
        {
            _cnBuilder.InitialCatalog = Database.Text;
        }

        private void Server_Validated(object sender, EventArgs e)
        {
            _cnBuilder.DataSource = Server.Text;
        }
    }
}
