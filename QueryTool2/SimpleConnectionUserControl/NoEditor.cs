using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace App.SimpleConnectionUserControl
{
    public partial class NoEditor : UserControl, ISimpleConnectionEdit
    {
        public NoEditor()
        {
            InitializeComponent();
        }

        public void EditConnection(System.Data.Common.DbProviderFactory factory, ConnectionInfo cninfo)
        {
            
        }
    }
}
