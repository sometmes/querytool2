using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QueryTool2
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
    }
}