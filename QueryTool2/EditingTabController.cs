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
            textEditorControl1.Document.UndoStack.ActionRedone +=new EventHandler(UndoStack_Action);
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
            return MessageBox.Show(this.tabPage3.Text + " " + SR.QuerySaveNow, SR.TitleQuerySaveNow, MessageBoxButtons.YesNoCancel);
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

        private void resultsTabControl_DoubleClick(object sender, EventArgs e)
        {
            object o = textEditorControl1.Document.UndoStack.UndoItemCount;
        }
    }
}
