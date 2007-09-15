using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QueryTool2
{
    public partial class ConnectionStringBuilderForm : Form
    {
        public DbConnectionStringBuilder ConnectionStringBuilder = null;

        public ConnectionStringBuilderForm()
        {
            InitializeComponent();
        }

        private void ConnectionStringBuilderForm_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = ConnectionStringBuilder;
            propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyGrid1_PropertyValueChanged);
            textBox1.Validated += new EventHandler(textBox1_Validated);
        }

        void textBox1_Validated(object sender, EventArgs e)
        {
            ConnectionStringBuilder.ConnectionString = textBox1.Text;
            propertyGrid1.Refresh();
        }

        void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            textBox1.Text = ConnectionStringBuilder.ConnectionString;
        }
    }
}