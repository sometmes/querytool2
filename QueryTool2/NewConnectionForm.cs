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
        ConnectionInfo _connection;

        public ConnectionInfo Connection
        {
            get { return _connection; }
            set
            {
                _connection = value;
                if (_connection != null)
                {
                    _factory = DbProviderFactories.GetFactory(_connection.ProviderInvariantName);
                    provider2Textbox.Text = _connection.ProviderName;
                    ShowEditorControl(_connection.ProviderInvariantName);
                    testConnection.Enabled = !string.IsNullOrEmpty(_connection.ConnectionString);
                }
            }
        }

        DbProviderFactory _factory = null;
        ISimpleConnectionEdit _editor = null;

        public NewConnectionForm()
        {
            InitializeComponent();
        }

        Size _dessignMinimumSize;
        private void NewConnectionForm_Load(object sender, EventArgs e)
        {
            _dessignMinimumSize = this.MinimumSize;
            Connection = My.Settings.LastConnection;
        }

        private void changeProvider2_Click(object sender, EventArgs e)
        {
            ChangeProviderForm f = new ChangeProviderForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Connection = f.SelectedConnection;
            }
        }

        void ShowEditorControl(string invariantName)
        {
            ISimpleConnectionEdit _editor = SimpleConnectionEditPlugin.CreateInstance(invariantName);
            UserControl editor = _editor as UserControl;
            editor.Location = new Point(6, 15);
            SimpleEditGroupBox.Controls.Clear();
            SimpleEditGroupBox.Controls.Add(editor);
            this.MinimumSize = _dessignMinimumSize;
            int xdiff = SimpleEditGroupBox.Width - (2 * 6) - editor.Width;
            this.Width -= xdiff;
            int ydiff = SimpleEditGroupBox.Height- (2 * 15) - editor.Height;
            this.Height -= ydiff;
            this.MinimumSize = this.Size;
        }

        private void advancedButton_Click(object sender, EventArgs e)
        {
            ConnectionStringBuilderForm f = new ConnectionStringBuilderForm();
            f.ConnectionStringBuilder = _factory.CreateConnectionStringBuilder();
            f.ConnectionStringBuilder.ConnectionString = _connection.ConnectionString;
            f.Text = _connection.ProviderName + " connection string properties.";
            if (f.ShowDialog() == DialogResult.OK)
            {
                _connection.UpdateConnectionString(f.ConnectionStringBuilder);
                testConnection.Enabled = !string.IsNullOrEmpty(_connection.ConnectionString);
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            My.Settings.LastConnection = _connection.Copy();
            My.Settings.RecentConnections.RegisterConnection(My.Settings.LastConnection);
        }

        EnabledState _enabledState;
        private void testConnection_Click(object sender, EventArgs e)
        {
            DbConnection cn = _factory.CreateConnection();
            cn.ConnectionString = _connection.ConnectionString;
            _enabledState = WinForms.DisableBut(cancelButton);
            cancelButton.DialogResult = DialogResult.None;
            testConnectWorker.RunWorkerAsync();
        }

        private class ConnectResult
        {
            public bool Success = false;
            public string Message;
        }

        private void testConnectWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DbConnection cn = _factory.CreateConnection();
            cn.ConnectionString = _connection.ConnectionString;
            try
            {
                cn.Open();
                ConnectResult r = new ConnectResult();
                r.Success = true;
                r.Message = "Connected successfully";
                e.Result = r;
            }
            catch (Exception ex)
            {
                ConnectResult r = new ConnectResult();
                r.Success=false;
                r.Message = "Test connection failed\r\n\r\n"+ex.GetBaseException().Message;
                e.Result = r;
            }
        }

        private void testconnectWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WinForms.DisableRestore(_enabledState);
            ConnectResult r = e.Result as ConnectResult;
            MessageBox.Show(r.Message, "Test Connection");
            cancelButton.DialogResult = DialogResult.Cancel;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            testConnectWorker.Abort();
            WinForms.DisableRestore(_enabledState);
            cancelButton.DialogResult = DialogResult.Cancel;
        }

    }
}