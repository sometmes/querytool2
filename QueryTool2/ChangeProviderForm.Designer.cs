namespace QueryTool2
{
    partial class ChangeProviderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listView1 = new System.Windows.Forms.ListView();
            this.cancelButton = new System.Windows.Forms.Button();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.recentConnString = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.newConn = new System.Windows.Forms.Button();
            this.lastConn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.provider2ComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(12, 31);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(361, 180);
            this.listView1.TabIndex = 0;
            this.listView1.TileSize = new System.Drawing.Size(300, 30);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Tile;
            this.listView1.Resize += new System.EventHandler(this.listView1_Resize);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.AutoSize = true;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(298, 379);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // recentConnString
            // 
            this.recentConnString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.recentConnString.Location = new System.Drawing.Point(12, 268);
            this.recentConnString.Multiline = true;
            this.recentConnString.Name = "recentConnString";
            this.recentConnString.ReadOnly = true;
            this.recentConnString.Size = new System.Drawing.Size(360, 88);
            this.recentConnString.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(11, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(361, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Last Connection Strings used with selected provider";
            // 
            // newConn
            // 
            this.newConn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.newConn.Location = new System.Drawing.Point(12, 379);
            this.newConn.Name = "newConn";
            this.newConn.Size = new System.Drawing.Size(108, 23);
            this.newConn.TabIndex = 10;
            this.newConn.Text = "New connection";
            this.newConn.UseVisualStyleBackColor = true;
            this.newConn.Click += new System.EventHandler(this.newConn_Click);
            // 
            // lastConn
            // 
            this.lastConn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lastConn.Location = new System.Drawing.Point(126, 379);
            this.lastConn.Name = "lastConn";
            this.lastConn.Size = new System.Drawing.Size(108, 23);
            this.lastConn.TabIndex = 10;
            this.lastConn.Text = "Last connection";
            this.lastConn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Installed .NET 2.0 Data Providers";
            // 
            // provider2ComboBox
            // 
            this.provider2ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.provider2ComboBox.FormattingEnabled = true;
            this.provider2ComboBox.Location = new System.Drawing.Point(12, 243);
            this.provider2ComboBox.Name = "provider2ComboBox";
            this.provider2ComboBox.Size = new System.Drawing.Size(360, 21);
            this.provider2ComboBox.TabIndex = 11;
            // 
            // ChangeProviderForm
            // 
            this.AcceptButton = this.lastConn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(385, 414);
            this.Controls.Add(this.provider2ComboBox);
            this.Controls.Add(this.lastConn);
            this.Controls.Add(this.newConn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.recentConnString);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.listView1);
            this.Name = "ChangeProviderForm";
            this.Text = "Change .NET 2.0 Data Provider";
            this.Load += new System.EventHandler(this.ChangeProviderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox recentConnString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button newConn;
        private System.Windows.Forms.Button lastConn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox provider2ComboBox;
    }
}