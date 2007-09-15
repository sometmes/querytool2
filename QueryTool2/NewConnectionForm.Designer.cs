namespace QueryTool2
{
    partial class NewConnectionForm
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
            this.provider2ComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.changeProvider2 = new System.Windows.Forms.Button();
            this.provider1ComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.changeProvider1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.testConnection = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.advancedButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // provider2ComboBox
            // 
            this.provider2ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.provider2ComboBox.FormattingEnabled = true;
            this.provider2ComboBox.Location = new System.Drawing.Point(12, 27);
            this.provider2ComboBox.Name = "provider2ComboBox";
            this.provider2ComboBox.Size = new System.Drawing.Size(279, 21);
            this.provider2ComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoEllipsis = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose a .NET 2.0 Data Provider or pick a recently used configuration";
            // 
            // changeProvider2
            // 
            this.changeProvider2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeProvider2.Location = new System.Drawing.Point(297, 25);
            this.changeProvider2.Name = "changeProvider2";
            this.changeProvider2.Size = new System.Drawing.Size(75, 23);
            this.changeProvider2.TabIndex = 2;
            this.changeProvider2.Text = "Change...";
            this.changeProvider2.UseVisualStyleBackColor = true;
            this.changeProvider2.Click += new System.EventHandler(this.changeProvider2_Click);
            // 
            // provider1ComboBox
            // 
            this.provider1ComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.provider1ComboBox.FormattingEnabled = true;
            this.provider1ComboBox.Location = new System.Drawing.Point(12, 79);
            this.provider1ComboBox.Name = "provider1ComboBox";
            this.provider1ComboBox.Size = new System.Drawing.Size(279, 21);
            this.provider1ComboBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoEllipsis = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(360, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Choose a .NET 1.0 Data Provider or pick a recently used configuration";
            // 
            // changeProvider1
            // 
            this.changeProvider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeProvider1.Location = new System.Drawing.Point(297, 77);
            this.changeProvider1.Name = "changeProvider1";
            this.changeProvider1.Size = new System.Drawing.Size(75, 23);
            this.changeProvider1.TabIndex = 2;
            this.changeProvider1.Text = "Change...";
            this.changeProvider1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 211);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configure provider";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(53, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(251, 89);
            this.label3.TabIndex = 0;
            this.label3.Text = "TODO: develop controls to edit connection properties for specific providers. Usin" +
                "g plug-in architecture.";
            // 
            // testConnection
            // 
            this.testConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.testConnection.AutoSize = true;
            this.testConnection.Location = new System.Drawing.Point(12, 379);
            this.testConnection.Name = "testConnection";
            this.testConnection.Size = new System.Drawing.Size(95, 23);
            this.testConnection.TabIndex = 5;
            this.testConnection.Text = "Test Connection";
            this.testConnection.UseVisualStyleBackColor = true;
            this.testConnection.Click += new System.EventHandler(this.testConnection_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptButton.AutoSize = true;
            this.acceptButton.Location = new System.Drawing.Point(216, 379);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 5;
            this.acceptButton.Text = "OK";
            this.acceptButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.AutoSize = true;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(297, 379);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // advancedButton
            // 
            this.advancedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.advancedButton.Location = new System.Drawing.Point(297, 329);
            this.advancedButton.Name = "advancedButton";
            this.advancedButton.Size = new System.Drawing.Size(75, 23);
            this.advancedButton.TabIndex = 6;
            this.advancedButton.Text = "Advanced...";
            this.advancedButton.UseVisualStyleBackColor = true;
            this.advancedButton.Click += new System.EventHandler(this.advancedButton_Click);
            // 
            // NewConnectionForm
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(387, 414);
            this.Controls.Add(this.advancedButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.testConnection);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.changeProvider1);
            this.Controls.Add(this.changeProvider2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.provider1ComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.provider2ComboBox);
            this.Name = "NewConnectionForm";
            this.Text = "Connection properties";
            this.Load += new System.EventHandler(this.NewConnectionForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox provider2ComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button changeProvider2;
        private System.Windows.Forms.ComboBox provider1ComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button changeProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button testConnection;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button advancedButton;
    }
}