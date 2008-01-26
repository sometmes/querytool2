using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace App
{
    public partial class QueryWindowForm : Form
    {
        Dictionary<TabPage, EditingTabController> editingTabList = new Dictionary<TabPage, EditingTabController>();

        public QueryWindowForm()
        {
            InitializeComponent();
        }

        private void QueryWindowForm_Load(object sender, EventArgs e)
        {
            filesTabControl.Dock = DockStyle.Fill;
            splitContainer2.Dock = DockStyle.Fill;
            treeView1.Dock = DockStyle.Fill;
            propertyGrid1.Dock = DockStyle.Fill;
            splitContainer3.Panel2Collapsed = true;

            FileNewCommand(null, null);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void newConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewConnectionForm f = new NewConnectionForm();
            f.ShowDialog();
        }

        private void splitContainer3_DoubleClick(object sender, EventArgs e)
        {
            splitContainer3.Panel2Collapsed = true;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void FileNewCommand(object sender, EventArgs e)
        {
            FileNewCommand(null);
        }

        private void FileNewCommand(string fileName)
        {
            EditingTabController c = new EditingTabController();
            c.FileName = fileName;
            TabPage tab = c.Tab;
            tab.Text = SR.NewFile + (filesTabControl.TabPages.Count + 1);
            filesTabControl.TabPages.Add(tab);
            editingTabList.Add(c.Tab, c);
            filesTabControl.SelectedTab = tab;
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                foreach (string filename in openFileDialog1.FileNames)
                {
                    FileNewCommand(filename);
                }
            }
        }
    }
}