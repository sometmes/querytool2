namespace App
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
            this.label1 = new System.Windows.Forms.Label();
            this.changeProvider2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.changeProvider1 = new System.Windows.Forms.Button();
            this.SimpleEditGroupBox = new System.Windows.Forms.GroupBox();
            this.testConnection = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.advancedButton = new System.Windows.Forms.Button();
            this.provider2Textbox = new System.Windows.Forms.TextBox();
            this.provider1Textbox = new System.Windows.Forms.TextBox();
            this.testConnectWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(326, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose a .NET 2.0 Data Provider or pick a recent configuration";
            // 
            // changeProvider2
            // 
            this.changeProvider2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeProvider2.Location = new System.Drawing.Point(240, 25);
            this.changeProvider2.Name = "changeProvider2";
            this.changeProvider2.Size = new System.Drawing.Size(75, 23);
            this.changeProvider2.TabIndex = 2;
            this.changeProvider2.Text = "Change...";
            this.changeProvider2.UseVisualStyleBackColor = true;
            this.changeProvider2.Click += new System.EventHandler(this.changeProvider2_Click);
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(323, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Choose a .NET 1.0 Data Provider or pick a recent configuration";
            // 
            // changeProvider1
            // 
            this.changeProvider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeProvider1.Location = new System.Drawing.Point(240, 79);
            this.changeProvider1.Name = "changeProvider1";
            this.changeProvider1.Size = new System.Drawing.Size(75, 23);
            this.changeProvider1.TabIndex = 2;
            this.changeProvider1.Text = "Change...";
            this.changeProvider1.UseVisualStyleBackColor = true;
            // 
            // SimpleEditGroupBox
            // 
            this.SimpleEditGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SimpleEditGroupBox.Location = new System.Drawing.Point(12, 112);
            this.SimpleEditGroupBox.Name = "SimpleEditGroupBox";
            this.SimpleEditGroupBox.Size = new System.Drawing.Size(303, 123);
            this.SimpleEditGroupBox.TabIndex = 3;
            this.SimpleEditGroupBox.TabStop = false;
            this.SimpleEditGroupBox.Text = "Configure provider";
            // 
            // testConnection
            // 
            this.testConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.testConnection.AutoSize = true;
            this.testConnection.Location = new System.Drawing.Point(326, 212);
            this.testConnection.Name = "testConnection";
            this.testConnection.Size = new System.Drawing.Size(95, 23);
            this.testConnection.TabIndex = 5;
            this.testConnection.Text = "Test Connection";
            this.testConnection.UseVisualStyleBackColor = true;
            this.testConnection.Click += new System.EventHandler(this.testConnection_Click);
            // 
            // connectButton
            // 
            this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectButton.AutoSize = true;
            this.connectButton.Location = new System.Drawing.Point(346, 25);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 5;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.AutoSize = true;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(346, 53);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // advancedButton
            // 
            this.advancedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.advancedButton.Location = new System.Drawing.Point(346, 183);
            this.advancedButton.Name = "advancedButton";
            this.advancedButton.Size = new System.Drawing.Size(75, 23);
            this.advancedButton.TabIndex = 6;
            this.advancedButton.Text = "Advanced...";
            this.advancedButton.UseVisualStyleBackColor = true;
            this.advancedButton.Click += new System.EventHandler(this.advancedButton_Click);
            // 
            // provider2Textbox
            // 
            this.provider2Textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.provider2Textbox.Location = new System.Drawing.Point(12, 27);
            this.provider2Textbox.Name = "provider2Textbox";
            this.provider2Textbox.ReadOnly = true;
            this.provider2Textbox.Size = new System.Drawing.Size(222, 20);
            this.provider2Textbox.TabIndex = 9;
            // 
            // provider1Textbox
            // 
            this.provider1Textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.provider1Textbox.Location = new System.Drawing.Point(12, 81);
            this.provider1Textbox.Name = "provider1Textbox";
            this.provider1Textbox.ReadOnly = true;
            this.provider1Textbox.Size = new System.Drawing.Size(222, 20);
            this.provider1Textbox.TabIndex = 10;
            // 
            // testConnectWorker
            // 
            this.testConnectWorker.WorkerSupportsCancellation = true;
            this.testConnectWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.testConnectWorker_DoWork);
            this.testConnectWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.testconnectWorker_RunWorkerCompleted);
            // 
            // NewConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 247);
            this.Controls.Add(this.provider1Textbox);
            this.Controls.Add(this.provider2Textbox);
            this.Controls.Add(this.advancedButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.testConnection);
            this.Controls.Add(this.SimpleEditGroupBox);
            this.Controls.Add(this.changeProvider1);
            this.Controls.Add(this.changeProvider2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(442, 274);
            this.Name = "NewConnectionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connection properties";
            this.Load += new System.EventHandler(this.NewConnectionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button changeProvider2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button changeProvider1;
        private System.Windows.Forms.GroupBox SimpleEditGroupBox;
        private System.Windows.Forms.Button testConnection;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button advancedButton;
        private System.Windows.Forms.TextBox provider2Textbox;
        private System.Windows.Forms.TextBox provider1Textbox;
        private System.ComponentModel.BackgroundWorker testConnectWorker;
    }
}