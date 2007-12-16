using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;

namespace App
{
    public partial class QueryWindowForm : Form
    {
        public QueryWindowForm()
        {
            InitializeComponent();
        }

        private void QueryWindowForm_Load(object sender, EventArgs e)
        {
            treeView1.Dock = DockStyle.Fill;
            propertyGrid1.Dock = DockStyle.Fill;
            resultsTabControl.Dock = DockStyle.Fill;
            textEditorControl1.Dock = DockStyle.Fill;

            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            HighlightingManager.Manager.AddSyntaxModeFileProvider(new FileSyntaxModeProvider(appPath));

            textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("LOGPARSER");
            textEditorControl1.ShowEOLMarkers = false;
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
    }
}