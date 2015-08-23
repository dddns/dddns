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
using DecimalDNSService;
using System.Runtime.InteropServices;

namespace DynamicDecimalDNSInstaller
{
    public partial class Form1 : Form
    {
        private const uint PM_REMOVE = 0x1;
        private const uint WM_MOUSEFIRST = 0x200;
        private const uint WM_MOUSELAST = 0x209;

        //// to clear queued mouse events
        //private struct Point
        //{
        //    long X;
        //    long Y;
        //}
        //private struct Message
        //{
        //    long hwnd;
        //    long message;
        //    long wParam;
        //    long lParam;
        //    long time;
        //    Point pt;
        //}
        //[DllImport("coredll")]
        //private extern static bool PeekMessage(out Message Msg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);
        //// to clear queued mouse events

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
            ButtonLockUnlock();

            try
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\settings.xml");
            }
            catch (Exception exp)
            {
                if ("1" == tools.logtofile) tools.WriteErrorLog(exp.Message);
            }

            try
            {
                XmlTextWriter writer = new XmlTextWriter(AppDomain.CurrentDomain.BaseDirectory + @"\settings.xml", System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement("dddnssettings");

                writer.WriteStartElement("logtofile");
                writer.WriteString((cbLogFile.Checked ? "1" : "0"));
                writer.WriteEndElement();

                writer.WriteStartElement("logtoeventviewer");
                writer.WriteString((cbLogEV.Checked ? "1" : "0"));
                writer.WriteEndElement();

                writer.WriteStartElement("hash");
                writer.WriteString(txtHash.Text);
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
            }
            catch (Exception exp)
            {
                if ("1" == tools.logtofile)
                {
                    tools.WriteErrorLog(exp.Message);
                }
            }

            Executa("setpermission.bat", "Setting file security permission right...", "");
            Executa("install.bat", "Installing service, please wait...", "Service DecimalDNSService has been successfully installed");
            ButtonLockUnlock();
        }

        private void ButtonLockUnlock()
        {
            btnInstall.Enabled = !btnInstall.Enabled;
            btnStartService.Enabled = !btnStartService.Enabled;
            btnStopService.Enabled = !btnStopService.Enabled;
            btnUnInstall.Enabled = !btnUnInstall.Enabled;
            Application.DoEvents();
        }


        private void SetNTFSPermission(string fileName, string account, FileSystemRights rights, AccessControlType controlType)
        {
            FileSecurity fSecurity = File.GetAccessControl(fileName);
            fSecurity.AddAccessRule(new FileSystemAccessRule(account, rights, controlType));
            File.SetAccessControl(fileName, fSecurity);
        }

        /// <summary>
        /// Executes each command/batch file to install service, start service, etc
        /// 
        /// </summary>
        /// <param name="filename">file to execute, usually batch file</param>
        /// <param name="message">message to show about this step</param>
        /// <param name="sucesstxt">sucess message to look for of command output to change color red or green</param>
        private void Executa(string filename, string message, string sucesstxt)
        {
            txtOutput.BackColor = Color.White;
            StringBuilder sb = new StringBuilder(message + "\n");
            txtOutput.Text = sb.ToString();
            Application.DoEvents();

            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = filename;
            p.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
            p.StartInfo.Verb = "runas";
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            p.Dispose();

            sb.Append(output + "\n" + "Done.");
            txtOutput.Text = sb.ToString();

            txtOutput.Focus();
            txtOutput.SelectionStart = txtOutput.Text.Length;
            txtOutput.ScrollToCaret();

            // check for sucess strings
            if (true == txtOutput.Text.Contains(sucesstxt)) txtOutput.BackColor = Color.LightGreen; else txtOutput.BackColor = Color.LightCoral;
            sb.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ButtonLockUnlock();
            Executa("startservice.bat", "Starting service, please wait...", "service was started successfully");
            ButtonLockUnlock();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ButtonLockUnlock();
            Executa("uninstall.bat", "Uninstall of service, please wait...", "was successfully removed from the system");
            ButtonLockUnlock();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ButtonLockUnlock();
            Executa("stopservice.bat", "Stopping the service, please wait...", "service was stopped successfully");
            ButtonLockUnlock();
        }

        public static bool IsElevated
        {
            get
            {
                return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
#if DEBUG
#else
            if (!IsElevated)
            {
                MessageBox.Show("Run as administrator with elevated permissions.");
                Application.Exit();
            }
            txtHash.Focus();
#endif
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form about = new AboutBox();
            about.ShowDialog();
            about.Dispose();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loadFromDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tools.GetXMLSettings();
            txtHash.Text = tools.hash;
            cbLogFile.Checked = ("1" == tools.logtofile ? true : false);
            cbLogEV.Checked = ("1" == tools.logtoEV ? true : false);
            txtInterval.Text = (tools.updateinterval / 60 / 1000).ToString();
            txtServerURL.Text = tools.serverurl;
            txtPublicIP.Text = tools.publicip;
        }
    }
}
