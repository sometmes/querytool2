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
        TabPage contextTabPage = null;

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

        private void ConnectCommand(object sender, EventArgs e)
        {
            NewConnectionForm f = new NewConnectionForm();
            if (DialogResult.OK == f.ShowDialog())
            {
                if (editingTabList.Count == 0)
                    FileNewCommand();
                EditingTabController cont = editingTabList[filesTabControl.SelectedTab];
                cont.Connection = f.Connection;
                cont.UpdateTabTitle();
            }
        }

        private void splitContainer3_DoubleClick(object sender, EventArgs e)
        {
            splitContainer3.Panel2Collapsed = true;
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filesTabControl.SelectedTab.ContextMenuStrip.Show();
        }

        private void FileNewCommand(object sender, EventArgs e)
        {
            FileNewCommand();
        }

        private void FileNewCommand()
        {
            EditingTabController c = new EditingTabController();
            c.FileName = null;
            TabPage tab = c.Tab;
            tab.Text = SR.NewFile + (filesTabControl.TabPages.Count + 1);
            filesTabControl.TabPages.Add(tab);
            editingTabList.Add(c.Tab, c);
            filesTabControl.SelectedTab = tab;
            c.Reload();
        }

        private void FileOpenCommand(string filename)
        {
            EditingTabController c;
            bool toadd = true;
            if (filesTabControl.TabCount > 0)
            {
                c = editingTabList[filesTabControl.SelectedTab];
                if (!(c.IsEmpty && c.FileName == null))
                    c = new EditingTabController();
                else
                    toadd = false;
            }
            else
                c = new EditingTabController();

            c.FileName = filename;
            TabPage tab = c.Tab;
            tab.Text = filename;
            if (toadd)
            {
                filesTabControl.TabPages.Add(tab);
                editingTabList.Add(c.Tab, c);
                filesTabControl.SelectedTab = tab;
            }
            c.Reload();
        }

        private void FileOpenCommand(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                foreach (string filename in openFileDialog1.FileNames)
                {
                    FileOpenCommand(filename);
                }
            }
        }

        private void filesTabControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextTabPage = TabAtPoint(filesTabControl, e.Location);
                if (this.contextTabPage!=null)
                    contextMenuStrip1.Show(filesTabControl, e.Location);
            }
        }

        private TabPage TabAtPoint(TabControl tabCtrl, Point location)
        {
            for (int i = 0; i < tabCtrl.TabCount; i++)
            {
                Rectangle r = tabCtrl.GetTabRect(i);
                if (r.Contains(location))
                {
                    return tabCtrl.TabPages[i];
                }
            }
            return null;
        }

        private void CloseTabCommand(object sender, EventArgs e)
        {
            if (this.contextTabPage == null)
                this.contextTabPage = filesTabControl.SelectedTab;

            editingTabList[this.contextTabPage].Close();
            if (!filesTabControl.TabPages.Contains(this.contextTabPage))
                editingTabList.Remove(this.contextTabPage);
            this.contextTabPage = null;
        }

        private void FileSaveCommand(object sender, EventArgs e)
        {
            if (this.contextTabPage == null)
                this.contextTabPage = filesTabControl.SelectedTab;

            editingTabList[this.contextTabPage].Save();

        }

        private void QueryWindowForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dictionary<TabPage, EditingTabController> newlist = new Dictionary<TabPage, EditingTabController>(this.editingTabList);
            foreach (KeyValuePair<TabPage,EditingTabController> pair in editingTabList)
            {
                pair.Value.Close();
                if (filesTabControl.TabPages.Contains(pair.Key))
                    break;
                newlist.Remove(pair.Key);
            }

            this.editingTabList = newlist;
            if (this.editingTabList.Count != 0)
                e.Cancel = true;
        }
    }
}