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
using System.IO;
using System.Xml;
using System.Security.AccessControl;

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
            XmlTextWriter writer = new XmlTextWriter(AppDomain.CurrentDomain.BaseDirectory + @"\settings.xml", System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("dddnssettings");

            writer.WriteStartElement("hash");
            writer.WriteString(txtHash.Text);
            writer.WriteEndElement();

            writer.WriteStartElement("logtofile");
            writer.WriteString((cbLogFile.Checked ? "1" : "0"));
            writer.WriteEndElement();

            writer.WriteStartElement("logtoeventviewer");
            writer.WriteString((cbLogEV.Checked ? "1" : "0"));
            writer.WriteEndElement();

            writer.WriteStartElement("updateinterval");
            int i = 0;
            int.TryParse(txtInterval.Text, out i);

            writer.WriteString((i * 60 * 1000).ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("serverurl");
            writer.WriteString(txtServerURL.Text);
            writer.WriteEndElement();

            writer.WriteStartElement("publicip");
            writer.WriteString(txtPublicIP.Text);
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

            string fileName = "DecimalDNSService.exe";
            SetNTFSPermission(AppDomain.CurrentDomain.BaseDirectory +
                            fileName, @"NT AUTHORITY\LOCAL SERVICE",
                            FileSystemRights.FullControl,
                            AccessControlType.Allow); // set "local service" to FULL for our service

            Executa("install.bat");
        }

        private void SetNTFSPermission(string fileName, string account,FileSystemRights rights, AccessControlType controlType)
        {
            FileSecurity fSecurity = File.GetAccessControl(fileName);
            fSecurity.AddAccessRule(new FileSystemAccessRule(account,rights, controlType));
            File.SetAccessControl(fileName, fSecurity);
        }

        //
        // adicionar "local service" icacls senão não tem permissão

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
            if (!IsElevated)
            {
                MessageBox.Show("Run as administrator with elevated permissions.");
                Application.Exit();
            }
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
