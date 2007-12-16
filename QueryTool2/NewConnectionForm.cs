using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace App
{
    public partial class NewConnectionForm : Form
    {
        DbProviderFactory _factory=null;
        string _connString = "";

        public NewConnectionForm()
        {
            InitializeComponent();
        }

        private void NewConnectionForm_Load(object sender, EventArgs e)
        {
            SimpleConnectionUserControl.MsSqlServer edit = new App.SimpleConnectionUserControl.MsSqlServer();
            edit.Location = new Point(6, 15);
            SimpleEditGroupBox.Controls.Add(edit);
            testConnection.Enabled = false;
        }

        private void changeProvider2_Click(object sender, EventArgs e)
        {
            ChangeProviderForm f = new ChangeProviderForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                _factory = DbProviderFactories.GetFactory(f.SelectedProvider);
                _connString = f.SelectedConnectionString;
                provider2ComboBox.Text = "New <" + f.SelectedProvider["Name"] + "> Connection";
                testConnection.Enabled = !string.IsNullOrEmpty(_connString);
            }
        }

        private void testConnection_Click(object sender, EventArgs e)
        {
            DbConnection cn = _factory.CreateConnection();
            cn.ConnectionString = _connString;
            try
            {
                cn.Open();
                MessageBox.Show("Connected successfully", "Test connection");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message, "Test connection failed");
            }

        }

        private void configureAdvanced_Click(object sender, EventArgs e)
        {

        }

        private void advancedButton_Click(object sender, EventArgs e)
        {
            ConnectionStringBuilderForm f = new ConnectionStringBuilderForm();
            f.ConnectionStringBuilder = _factory.CreateConnectionStringBuilder();
            f.ConnectionStringBuilder.ConnectionString = _connString;
            if (f.ShowDialog() == DialogResult.OK)
            {
                _connString = f.ConnectionStringBuilder.ConnectionString;
            }
        }

    }
}