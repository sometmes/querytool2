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
        DataTable providerList;

        public ChangeProviderForm()
        {
            InitializeComponent();
        }

        public DataRow SelectedProvider;
        public string SelectedConnectionString;

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
            //si es buida, canviar botó per defecte a 'newConn' si no a 'lastConn'

            this.AcceptButton = newConn;
            lastConn.Enabled = false;
            if (listView1.SelectedItems.Count == 0)
                (this.AcceptButton as Button).Enabled = false;
            else
                (this.AcceptButton as Button).Enabled = true;
        }

        private void newConn_Click(object sender, EventArgs e)
        {
            SelectedProvider = listView1.SelectedItems[0].Tag as DataRow;
            this.DialogResult = DialogResult.OK;
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            newConn_Click(null, null);
        }
    }
}