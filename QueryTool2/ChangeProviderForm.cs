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
    public partial class ChangeProviderForm : Form
    {
        public ConnectionInfo SelectedConnection;
        DataTable providerList;
        ConnectionInfoList _cl;

        public ChangeProviderForm()
        {
            InitializeComponent();
        }

        private void ChangeProviderForm_Load(object sender, EventArgs e)
        {
            providerList = DbProviderFactories.GetFactoryClasses();
            foreach (DataRow provider in providerList.Rows)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = provider;
                item.Text = provider["Name"] as string;
                item.SubItems.Add(provider["Description"] as string,SystemColors.Control,SystemColors.Window,listView1.Font);
                listView1.Items.Add(item);
            }
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            listView1.TileSize = new Size(listView1.ClientSize.Width-10, listView1.TileSize.Height);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;

            DataRow selectedProvider = listView1.SelectedItems[0].Tag as DataRow;
            string sel = selectedProvider["InvariantName"] as string;
            if (My.Settings.RecentConnections.Contains(sel))
                _cl = My.Settings.RecentConnections[sel];
            else
                _cl = null;
            //Recuperar connection string recent
            //si es buida, canviar botó per defecte a 'newConn' si no a 'lastConn'
            if (_cl == null)
            {
                this.AcceptButton = newConn;
                lastConn.Enabled = false;
            }
            else
            {
                this.AcceptButton = lastConn;
                newConn.Enabled = false;

                RecentScroll.Maximum = _cl.Connections.Count-1;
                RecentScroll.Minimum = 0;
                RecentScroll_ValueChanged(null, null);
            }

            (this.AcceptButton as Button).Enabled = true;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            newConn_Click(null, null);
        }

        private void RecentScroll_ValueChanged(object sender, EventArgs e)
        {
            recentConnString.Text = _cl.Connections[RecentScroll.Value].ConnectionString;
        }

        private void newConn_Click(object sender, EventArgs e)
        {
            DataRow selectedProvider = listView1.SelectedItems[0].Tag as DataRow;
            SelectedConnection = new ConnectionInfo();
            SelectedConnection.SupportsProviderFactory = true;
            SelectedConnection.ProviderInvariantName = selectedProvider["InvariantName"] as string;
            SelectedConnection.ProviderName = selectedProvider["Name"] as string;
            this.DialogResult = DialogResult.OK;
        }

        private void lastConn_Click(object sender, EventArgs e)
        {
            SelectedConnection = _cl.Connections[RecentScroll.Value].Copy();
            this.DialogResult = DialogResult.OK;
        }
    }
}