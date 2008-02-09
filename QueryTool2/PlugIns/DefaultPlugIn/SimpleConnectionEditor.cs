using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using QueryTool2.Plugins;

namespace QueryTool2.DefaultPlugIn
{
    public partial class SimpleConnectionEditor : UserControl, ISimpleConnectionEdit
    {
        public SimpleConnectionEditor()
        {
            InitializeComponent();
        }

        public void EditConnection(System.Data.Common.DbProviderFactory factory, IConnectionInfo cninfo)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
