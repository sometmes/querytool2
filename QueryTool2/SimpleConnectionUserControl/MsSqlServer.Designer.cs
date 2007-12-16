namespace App.SimpleConnectionUserControl
{
    partial class MsSqlServer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Database = new System.Windows.Forms.ComboBox();
            this.Server = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Pwd = new System.Windows.Forms.TextBox();
            this.Login = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Database
            // 
            this.Database.FormattingEnabled = true;
            this.Database.Location = new System.Drawing.Point(65, 35);
            this.Database.Name = "Database";
            this.Database.Size = new System.Drawing.Size(110, 21);
            this.Database.TabIndex = 20;
            // 
            // Server
            // 
            this.Server.FormattingEnabled = true;
            this.Server.Location = new System.Drawing.Point(65, 6);
            this.Server.Name = "Server";
            this.Server.Size = new System.Drawing.Size(110, 21);
            this.Server.TabIndex = 21;
            this.Server.Text = "serverAAA";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Database:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Server:";
            // 
            // Pwd
            // 
            this.Pwd.Location = new System.Drawing.Point(65, 91);
            this.Pwd.Name = "Pwd";
            this.Pwd.Size = new System.Drawing.Size(110, 20);
            this.Pwd.TabIndex = 16;
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(65, 62);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(110, 20);
            this.Login.TabIndex = 17;
            this.Login.Text = "test004";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Login:";
            // 
            // MsSqlServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.Database);
            this.Controls.Add(this.Server);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Pwd);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Name = "MsSqlServer";
            this.Size = new System.Drawing.Size(313, 135);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Database;
        private System.Windows.Forms.ComboBox Server;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Pwd;
        private System.Windows.Forms.TextBox Login;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}
