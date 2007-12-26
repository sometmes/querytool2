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
        ISimpleConnectionEdit _editor = null;

        public NewConnectionForm()
        {
            InitializeComponent();
        }

        Size _dessignMinimumSize;
        private void NewConnectionForm_Load(object sender, EventArgs e)
        {
            _dessignMinimumSize = this.MinimumSize;
            //SimpleConnectionUserControl.MsSqlServer edit = new App.SimpleConnectionUserControl.MsSqlServer();
            //edit.Location = new Point(6, 15);
            //SimpleEditGroupBox.Controls.Add(edit);
            //testConnection.Enabled = false;
        }

        private void changeProvider2_Click(object sender, EventArgs e)
        {
            ChangeProviderForm f = new ChangeProviderForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                _factory = DbProviderFactories.GetFactory(f.SelectedProvider);
                _connString = f.SelectedConnectionString;
                provider2Textbox.Text = f.SelectedProvider["Name"] as string;
                ShowEditorControl(f.SelectedProvider["InvariantName"] as string);
                testConnection.Enabled = !string.IsNullOrEmpty(_connString);
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