using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using System.IO;

namespace App
{
    public partial class EditingTabController : UserControl
    {
        private string fileName;
        int lastUndoItemCount;
        ConnectionInfo connection;
        TabTitleStyleEnum tabTitleStyle = TabTitleStyleEnum.FileName | TabTitleStyleEnum.Server | TabTitleStyleEnum.Database;
        StatementExecutionController _execController = new StatementExecutionController();

        public delegate void RowCountChangeDelegate(int rowcount,bool loading);
        public event RowCountChangeDelegate RowCountChange;

        EnabledState _enabledState = new EnabledState();

        public EditingTabController()
        {
            InitializeComponent();

            splitContainer1.Dock = DockStyle.Fill;
            resultsTabControl.Dock = DockStyle.Fill;
            textEditorControl1.Dock = DockStyle.Fill;

            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            HighlightingManager.Manager.AddSyntaxModeFileProvider(new FileSyntaxModeProvider(appPath));

            textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("LOGPARSER");
            textEditorControl1.ShowEOLMarkers = false;
            textEditorControl1.Document.DocumentChanged += new DocumentEventHandler(Document_DocumentChanged);
            textEditorControl1.Document.UndoStack.ActionUndone += new EventHandler(UndoStack_Action);
            textEditorControl1.Document.UndoStack.ActionRedone += new EventHandler(UndoStack_Action);
            textEditorControl1.ActiveTextAreaControl.TextArea.KeyPress += new KeyPressEventHandler(TextArea_KeyPress);
            textEditorControl1.ActiveTextAreaControl.TextArea.KeyDown += new KeyEventHandler(TextArea_KeyDown);
            

            resultsTabControl.TabPages.Clear();
            _execController = new StatementExecutionController();
            _execController.ExecuteAsyncNewResult += new StatementExecutionController.ExecuteAsyncNewResultDelegate(_execController_ExecuteAsyncNewResult);
            _execController.Start += new StatementExecutionController.StartDelegate(_execController_Start);
            _execController.End += new StatementExecutionController.EndDelegate(_execController_End);
            _execController.ExecuteAsyncRowFetchComplete += new StatementExecutionController.ExecuteAsyncRowFetchCompleteDelegate(_execController_ExecuteAsyncRowFetchComplete);

            textEditorControl1.Text = @"
select * from AdventureWorks.Person.AddressType
--select * from AdventureWorks.Production.WorkOrder
";

            CreateGridTab();
        }

        void TextArea_KeyDown(object sender, KeyEventArgs e)
        {

        }

        void TextArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        public EnabledState EnabledState
        {
            get { return _enabledState; }
        }

        internal StatementExecutionController ExecController
        {
            get { return _execController; }
        }


        [Flags]
        public enum TabTitleStyleEnum
        {
            FileName,
            FilePath,
            Server,
            Database,
            DbUser
        }

        public void UpdateTabTitle()
        {
            string tabtitle= "";
            if (this.fileName != null && (this.tabTitleStyle & TabTitleStyleEnum.FilePath) == TabTitleStyleEnum.FilePath)
                tabtitle += Path.GetDirectoryName(this.fileName);
            if (this.fileName != null && (this.tabTitleStyle & TabTitleStyleEnum.FileName) == TabTitleStyleEnum.FileName)
                tabtitle += (tabtitle == "" ? "" : "\\") + Path.GetFileName(this.fileName);
            if (this.connection != null && (this.tabTitleStyle & TabTitleStyleEnum.Server) == TabTitleStyleEnum.Server)
                tabtitle += (tabtitle == "" ? "" : ":") + connection.ConnectionProperties.Server;
            if (this.connection != null && (this.tabTitleStyle & TabTitleStyleEnum.Database) == TabTitleStyleEnum.Database)
                tabtitle += (tabtitle == "" ? "" : ":") + connection.ConnectionProperties.Database;
            if (this.connection != null && (this.tabTitleStyle & TabTitleStyleEnum.DbUser) == TabTitleStyleEnum.DbUser)
                tabtitle += (tabtitle == "" ? "" : ":") + connection.ConnectionProperties.UserName;
            if (tabtitle != "")
                tabPage3.Text = tabtitle;
        }

        public TabTitleStyleEnum TabTitleStyle
        {
            get { return tabTitleStyle; }
            set { tabTitleStyle = value; }
        }

        public ConnectionInfo Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public string Statement
        {
            get 
            {
                string sel = "";
                sel = textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText;
                if (string.IsNullOrEmpty(sel))
                    sel = textEditorControl1.Text;
                return sel;
            }
        }

        void _execController_Start()
        {
            resultsTabControl.TabPages.Clear();
            _resultsCount = -1;
        }

        void _execController_End()
        {
            timerRowCount.Enabled = false;
        }

        void UndoStack_Action(object sender, EventArgs e)
        {
            if (this.lastUndoItemCount == textEditorControl1.Document.UndoStack.UndoItemCount)
                RemoveChangedMark();
        }

        private void RemoveChangedMark()
        {
            string text = this.tabPage3.Text;
            if (text.EndsWith("*"))
                this.tabPage3.Text = text.Remove(text.Length - 1);
        }

        void Document_DocumentChanged(object sender, DocumentEventArgs e)
        {
            string text = this.tabPage3.Text;
            if (!text.EndsWith("*"))
                this.tabPage3.Text = text + "*";
        }

        public TabPage Tab
        {
            get { return tabPage3; }
        }

        public bool Save()
        {
            if (fileName == null)
                fileName = UserQuerySave();
            if (fileName == null)
                return false;
            textEditorControl1.SaveFile(this.fileName);
            this.lastUndoItemCount = textEditorControl1.Document.UndoStack.UndoItemCount;

            RemoveChangedMark();

            return true;
        }

        private string UserQuerySave()
        {
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
                return saveFileDialog1.FileName;
            else
                return null;
        }

        private DialogResult UserQueryClose()
        {
            return MessageBox.Show("\"" + this.tabPage3.Text + "\"" + " " + SR.QuerySaveNow, SR.TitleQuerySaveNow, MessageBoxButtons.YesNoCancel);
        }

        public void Close()
        {
            if (textEditorControl1.Document.UndoStack.UndoItemCount != this.lastUndoItemCount)
            {
                DialogResult res = UserQueryClose();
                if (res == DialogResult.Yes)
                {
                    if (false == Save())
                        return;
                }
                else if (res == DialogResult.Cancel)
                {
                    return;
                }
            }
            TabControl tabCtrl = this.tabPage3.Parent as TabControl;
            tabCtrl.TabPages.Remove(this.tabPage3);
        }

        public bool IsEmpty
        {
            get
            {
                return textEditorControl1.Document.TextLength == 0;
            }
        }

        public void Reload()
        {
            if (string.IsNullOrEmpty(this.FileName)) return;

            textEditorControl1.LoadFile(this.fileName);
            this.lastUndoItemCount = textEditorControl1.Document.UndoStack.UndoItemCount;
            RemoveChangedMark();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
        }

        public void SwitchPane()
        {
            if (textEditorControl1.ActiveTextAreaControl.TextArea.Focused == true)
            {
                TabPage gridTab = resultsTabControl.SelectedTab;
                if (gridTab == null) return;
                TabControl tc = gridTab.Controls[0] as TabControl;
                TabPage page = tc.SelectedTab;
                if (page == null) return;
                Control grid = page.Controls[0];
                grid.Focus();
            }
            else
            {
                textEditorControl1.Focus();
            }
        }

        private void resultsTabControl_DoubleClick(object sender, EventArgs e)
        {
            object o = textEditorControl1.Document.UndoStack.UndoItemCount;
        }

        int _resultsCount = -1;
        TabPage CreateGridTab()
        {
            TabPage page = new TabPage("Resultset " + (_resultsCount + 1));
            TabControl dataAndSchema = new TabControl();
            dataAndSchema.Selected += new TabControlEventHandler(dataAndSchema_Selected);
            dataAndSchema.Dock = DockStyle.Fill;
            TabPage data = new TabPage("Data");
            data.Name = "dataTabPage";
            PagedGridView grid = new PagedGridView();
            grid.DoubleClick += new EventHandler(grid_DoubleClick);
            grid.Dock = DockStyle.Fill;
            data.Controls.Add(grid);
            TabPage schema = new TabPage("Schema");
            schema.Name = "schemaTabPage";
            DataGridView schgrid = new DataGridView();
            schgrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            schgrid.Dock = DockStyle.Fill;
            schema.Controls.Add(schgrid);
            dataAndSchema.TabPages.Add(data);
            dataAndSchema.TabPages.Add(schema);
            page.Controls.Add(dataAndSchema);
            resultsTabControl.TabPages.Add(page);
            dataAndSchema.SelectedTab = data;
            return page;
        }

        void dataAndSchema_Selected(object sender, TabControlEventArgs e)
        {
            int rowcount = -1;
            DataTable t = VisibleDataTable;
            if (t != null)
                rowcount = t.Rows.Count;
            if (RowCountChange != null)
                RowCountChange.Invoke(rowcount, VisibleDataTable == _loadingDataTable);
        }

        void grid_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show((sender as DataGridView).FirstDisplayedScrollingRowIndex.ToString());
        }

        PagedGridView GetDataGrid(TabPage gridTab)
        {
            if (gridTab == null) return null;
            TabControl tc = gridTab.Controls[0] as TabControl;
            return tc.TabPages["dataTabPage"].Controls[0] as PagedGridView;
        }

        DataGridView GetSchemaGrid(TabPage gridTab)
        {
            if (gridTab == null) return null;
            TabControl tc = gridTab.Controls[0] as TabControl;
            return tc.TabPages["schemaTabPage"].Controls[0] as DataGridView;
        }

        public DataTable VisibleDataTable
        {
            get
            {
                if (resultsTabControl.SelectedTab == null) return null;
                return GetDataGrid(resultsTabControl.SelectedTab).PagedDataSource as DataTable;
            }
        }

        void CreateTextTab()
        {
            TabPage page = new TabPage("Messages");
            TextBox txt = new TextBox();
            txt.Multiline = true;
            txt.Dock = DockStyle.Fill;
            page.Controls.Add(txt);
            resultsTabControl.TabPages.Add(page);
        }

        DataTable _loadingDataTable = null;

        void _execController_ExecuteAsyncNewResult(StatementExecutionController.ExecuteNewResultEventArgs e)
        {
            _resultsCount++;
            TabPage tab = CreateGridTab();
            if (timerRowCount.Enabled == false)
                timerRowCount.Enabled = true;

            PagedGridView grid = GetDataGrid(tab);
            DataGridViewColumn col;
            foreach (DataRow schemarow in e.Schema.Rows)
            {
                col = new DataGridViewTextBoxColumn();
                col.Name = (string)schemarow["ColumnName"]+"column";
                col.HeaderText = (string)schemarow["ColumnName"];
                col.DataPropertyName = (string)schemarow["ColumnName"];
                col.ReadOnly = (bool)schemarow["IsReadOnly"];
                grid.Columns.Add(col);
            }

            DataGridView schgrid = GetSchemaGrid(tab);
            schgrid.DataSource = e.Schema;

            grid.PagedDataSource = e.Data;
            _loadingDataTable = e.Data;
        }

        void _execController_ExecuteAsyncRowFetchComplete()
        {
            _loadingDataTable = null;
            timerRowCount_Tick(null, null);
            PagedGridView grid = GetDataGrid(resultsTabControl.SelectedTab);
            grid.PageNeeded -= new PagedGridView.PageNeededDelegate(grid_PageNeeded);
            grid.NoMorePagesAvailable();
        }

        private void timerRowCount_Tick(object sender, EventArgs e)
        {
            int rowcount = -1;
            lock (_execController.DataSet)
            {
                if (_loadingDataTable != null)
                    rowcount = _loadingDataTable.Rows.Count;
            }

            PagedGridView grid = GetDataGrid(resultsTabControl.SelectedTab);
            
            if (grid.RowCount == 0)
                grid.RowCount = rowcount;

            grid.PageNeeded += new PagedGridView.PageNeededDelegate(grid_PageNeeded);

            if (_loadingDataTable != null && VisibleDataTable == _loadingDataTable)
            {
                if (RowCountChange != null)
                    RowCountChange.Invoke(rowcount, VisibleDataTable == _loadingDataTable);
            }
            else
            {
                if (RowCountChange != null)
                    RowCountChange.Invoke(-1,false);
            }
        }

        void grid_PageNeeded(int rowIndex, int pageSize)
        {
            int rowcount = -1;
            lock (_execController.DataSet)
            {
                if (_loadingDataTable != null)
                    rowcount = _loadingDataTable.Rows.Count;
            }

            PagedGridView grid = GetDataGrid(resultsTabControl.SelectedTab);

            grid.RowCount = rowcount;

        }
    }
}
