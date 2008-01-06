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
            //TODO: recuperar connection string recent
            //si es buida, canviar bot� per defecte a 'newConn' si no a 'lastConn'

            this.AcceptButton = newConn;
            lastConn.Enabled = false;
            if (listView1.SelectedItems.Count == 0)
                (this.AcceptButton as Button).Enabled = false;
            else
                (this.AcceptButton as Button).Enabled = true;
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

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            newConn_Click(null, null);
        }
    }
}