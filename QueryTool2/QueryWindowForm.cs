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
        Dictionary<TabPage, EditingTabController> _editingTabList = new Dictionary<TabPage, EditingTabController>();
        TabPage _contextTabPage = null;
        StatementExecutionController _execController = new StatementExecutionController();

        public QueryWindowForm()
        {
            InitializeComponent();
            _execController.Start += new StatementExecutionController.StartDelegate(_execController_Start);
            _execController.End += new StatementExecutionController.EndDelegate(_execController_End);
            filesTabControl.Selected += new TabControlEventHandler(filesTabControl_Selected);
            filesTabControl.Deselected += new TabControlEventHandler(filesTabControl_Deselected);
            toolStripRowCount.Text = "";
        }

        void filesTabControl_Deselected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == null) return;
            EditingTabController cont = _editingTabList[e.TabPage];
            if (cont == null) return;
            cont.RowCountChange -= new EditingTabController.RowCountChangeDelegate(QueryWindowForm_RowCountChange);
        }

        void filesTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == null) return;
            EditingTabController cont = _editingTabList[e.TabPage];
            if (cont == null) return;
            cont.RowCountChange += new EditingTabController.RowCountChangeDelegate(QueryWindowForm_RowCountChange);
        }

        void QueryWindowForm_RowCountChange(int rowcount)
        {
            if (rowcount != -1)
            {
                if (toolStripStatusLabel1.Text == "toolStripStatusLabel1")
                    toolStripStatusLabel1.Text = rowcount.ToString();
                toolStripRowCount.Text = rowcount.ToString();
            }
            else
                toolStripRowCount.Text = "";
        }

        EnabledState _enabledState;
        void _execController_Start()
        {

        }

        void _execController_End()
        {

        }

        private void QueryWindowForm_Load(object sender, EventArgs e)
        {
            filesTabControl.Dock = DockStyle.Fill;
            splitContainer2.Dock = DockStyle.Fill;
            treeView1.Dock = DockStyle.Fill;
            propertyGrid1.Dock = DockStyle.Fill;
            splitContainer3.Panel2Collapsed = true;

            FileNewCommand(null, null);
            EditingTabController cont = _editingTabList[filesTabControl.SelectedTab];
            cont.RowCountChange += new EditingTabController.RowCountChangeDelegate(QueryWindowForm_RowCountChange);
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
                if (_editingTabList.Count == 0)
                    FileNewCommand();
                _execController.Connection = f.Connection.OpenConnection;
                EditingTabController cont = _editingTabList[filesTabControl.SelectedTab];
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
            c.ExecController = _execController;
            TabPage tab = c.Tab;
            tab.Text = SR.NewFile + (filesTabControl.TabPages.Count + 1);
            filesTabControl.TabPages.Add(tab);
            _editingTabList.Add(c.Tab, c);
            filesTabControl.SelectedTab = tab;
            c.Reload();
        }

        private void FileOpenCommand(string filename)
        {
            EditingTabController c;
            bool toadd = true;
            if (filesTabControl.TabCount > 0)
            {
                c = _editingTabList[filesTabControl.SelectedTab];
                if (!(c.IsEmpty && c.FileName == null))
                    c = new EditingTabController();
                else
                    toadd = false;
            }
            else
                c = new EditingTabController();

            c.FileName = filename;
            c.UpdateTabTitle();
            TabPage tab = c.Tab;
            if (toadd)
            {
                filesTabControl.TabPages.Add(tab);
                _editingTabList.Add(c.Tab, c);
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
                _contextTabPage = TabAtPoint(filesTabControl, e.Location);
                if (this._contextTabPage!=null)
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
            if (this._contextTabPage == null)
                this._contextTabPage = filesTabControl.SelectedTab;

            _editingTabList[this._contextTabPage].Close();
            if (!filesTabControl.TabPages.Contains(this._contextTabPage))
                _editingTabList.Remove(this._contextTabPage);
            this._contextTabPage = null;
        }

        private void FileSaveCommand(object sender, EventArgs e)
        {
            if (this._contextTabPage == null)
                this._contextTabPage = filesTabControl.SelectedTab;

            _editingTabList[this._contextTabPage].Save();

        }

        private void QueryWindowForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dictionary<TabPage, EditingTabController> newlist = new Dictionary<TabPage, EditingTabController>(this._editingTabList);
            foreach (KeyValuePair<TabPage,EditingTabController> pair in _editingTabList)
            {
                pair.Value.Close();
                if (filesTabControl.TabPages.Contains(pair.Key))
                    break;
                newlist.Remove(pair.Key);
            }

            this._editingTabList = newlist;
            if (this._editingTabList.Count != 0)
                e.Cancel = true;
        }

        private void executeToolStripButton_Click(object sender, EventArgs e)
        {
            _execController.ExecuteAsync(_editingTabList[filesTabControl.SelectedTab].Statement);
        }

    }
}