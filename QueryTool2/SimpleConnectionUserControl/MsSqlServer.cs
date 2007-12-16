using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SqlClient;

namespace App.SimpleConnectionUserControl
{
    public partial class MsSqlServer : UserControl, ISimpleConnectionEdit
    {
        DbProviderFactory _factory;
        RecentConnection _connectionInfo;
        SqlConnectionStringBuilder _cnBuilder;

        public MsSqlServer()
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

        public void EditConnection(DbProviderFactory factory, RecentConnection cninfo)
        {
            _factory = factory;
            _connectionInfo = cninfo;
            _cnBuilder = _factory.CreateConnectionStringBuilder() as SqlConnectionStringBuilder;
            _cnBuilder.ConnectionString = _connectionInfo.ConnectionString;
            BindControls();
        }
    }
}
