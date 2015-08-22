using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Configuration.Install;
using System.Web;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Diagnostics;

namespace DynamicDecimalDNSInstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// install service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Executa("install.bat");
        }
                
        private void Executa(string filename)
        {
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = filename;
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            txtOutput.Text = output;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Executa("startservice.bat");
        }

        public static bool IsElevated
        {
            get
            {
                return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
        private static int InstallService()
        {
            

            return 0;
        }

        private static int UninstallService()
        {
          

            return 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //if (!IsElevated)
            //{
            //    MessageBox.Show("Run as administrator with elevated permissions.");
            //    Application.Exit();
            //}
            txtHash.Focus();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Executa("uninstall.bat");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Executa("stopservice.bat");
        }
    }
}
