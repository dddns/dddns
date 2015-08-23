namespace DynamicDecimalDNSInstaller
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtHash = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtServerURL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPublicIP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbLogFile = new System.Windows.Forms.CheckBox();
            this.cbLogEV = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hash";
            // 
            // txtHash
            // 
            this.txtHash.Location = new System.Drawing.Point(78, 6);
            this.txtHash.Name = "txtHash";
            this.txtHash.Size = new System.Drawing.Size(708, 20);
            this.txtHash.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(78, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(157, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "Install service";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(241, 171);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 29);
            this.button2.TabIndex = 7;
            this.button2.Text = "Start service";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(241, 206);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(107, 29);
            this.button3.TabIndex = 9;
            this.button3.Text = "Stop service";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(78, 206);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(157, 29);
            this.button4.TabIndex = 8;
            this.button4.Text = "UNInstall service";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Log EV";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(78, 84);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(69, 20);
            this.txtInterval.TabIndex = 13;
            this.txtInterval.Text = "60";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Interval";
            // 
            // txtServerURL
            // 
            this.txtServerURL.Location = new System.Drawing.Point(78, 110);
            this.txtServerURL.Name = "txtServerURL";
            this.txtServerURL.Size = new System.Drawing.Size(270, 20);
            this.txtServerURL.TabIndex = 15;
            this.txtServerURL.Text = "http://adelinoaraujo.com/post.php?hash=";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Server URL";
            // 
            // txtPublicIP
            // 
            this.txtPublicIP.Location = new System.Drawing.Point(78, 136);
            this.txtPublicIP.Name = "txtPublicIP";
            this.txtPublicIP.Size = new System.Drawing.Size(270, 20);
            this.txtPublicIP.TabIndex = 17;
            this.txtPublicIP.Text = "http://decimal.pt/get.php";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Public IP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Log file";
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(367, 32);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(419, 203);
            this.txtOutput.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(153, 87);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "update time in minuts";
            // 
            // cbLogFile
            // 
            this.cbLogFile.AutoSize = true;
            this.cbLogFile.Location = new System.Drawing.Point(78, 35);
            this.cbLogFile.Name = "cbLogFile";
            this.cbLogFile.Size = new System.Drawing.Size(255, 17);
            this.cbLogFile.TabIndex = 22;
            this.cbLogFile.Text = "create a text log in disk, good for troubleshooting";
            this.cbLogFile.UseVisualStyleBackColor = true;
            // 
            // cbLogEV
            // 
            this.cbLogEV.AutoSize = true;
            this.cbLogEV.Checked = true;
            this.cbLogEV.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLogEV.Location = new System.Drawing.Point(78, 61);
            this.cbLogEV.Name = "cbLogEV";
            this.cbLogEV.Size = new System.Drawing.Size(134, 17);
            this.cbLogEV.TabIndex = 23;
            this.cbLogEV.Text = "log to the event viewer";
            this.cbLogEV.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 245);
            this.Controls.Add(this.cbLogEV);
            this.Controls.Add(this.cbLogFile);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPublicIP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtServerURL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtHash);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Dynamic Decimal DNS Installer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHash;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtServerURL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPublicIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbLogFile;
        private System.Windows.Forms.CheckBox cbLogEV;
    }
}

