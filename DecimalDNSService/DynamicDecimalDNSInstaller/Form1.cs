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

           
        }
                
        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = IsElevated.ToString();
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
    }
}
