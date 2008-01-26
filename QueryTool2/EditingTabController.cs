using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;

namespace App
{
    public partial class EditingTabController : UserControl
    {
        private string fileName;

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
        }

        public TabPage Tab
        {
            get { return tabPage3; }
        }

        public void Save()
        {
        }

        public void Close()
        {
        }

        public void Reload()
        {
            if (string.IsNullOrEmpty(this.FileName)) return;

            textEditorControl1.LoadFile(this.fileName);
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
        }
    }
}
