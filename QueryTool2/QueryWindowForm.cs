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

        private ICSharpCode.TextEditor.TextEditorControl CreateTextEditor()
        {
            ICSharpCode.TextEditor.TextEditorControl textEditorControl1 = new ICSharpCode.TextEditor.TextEditorControl();
            textEditorControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textEditorControl1.IsIconBarVisible = false;
            textEditorControl1.Location = new System.Drawing.Point(32, 31);
            textEditorControl1.Name = "textEditorControl1";
            textEditorControl1.ShowEOLMarkers = true;
            textEditorControl1.ShowLineNumbers = false;
            textEditorControl1.ShowSpaces = true;
            textEditorControl1.ShowTabs = true;
            textEditorControl1.ShowVRuler = true;
            textEditorControl1.Size = new System.Drawing.Size(280, 176);
            textEditorControl1.TabIndent = 2;
            textEditorControl1.TabIndex = 0;

            textEditorControl1.Dock = DockStyle.Fill;

            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            HighlightingManager.Manager.AddSyntaxModeFileProvider(new FileSyntaxModeProvider(appPath));

            textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("LOGPARSER");
            textEditorControl1.ShowEOLMarkers = false;


            return textEditorControl1;
            //this.splitContainer3.Panel1.Controls.Add(this.textEditorControl1);

        }

        private void QueryWindowForm_Load(object sender, EventArgs e)
        {
            filesTabControl.Dock = DockStyle.Fill;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer2.Dock = DockStyle.Fill;
            treeView1.Dock = DockStyle.Fill;
            propertyGrid1.Dock = DockStyle.Fill;
            resultsTabControl.Dock = DockStyle.Fill;
            splitContainer3.Panel2Collapsed = true;
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
    }
}