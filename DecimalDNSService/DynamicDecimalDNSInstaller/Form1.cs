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

namespace DynamicDecimalDNSInstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (WebClient wclient = new WebClient())
            {
                System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("hash", "vmS9bEB9E6bPmgMPBnB1bsrvlotDIHatHW0x9xvqeeuwAtBjwJC5tvQUnFltXg0f");
                byte[] responsebytes = wclient.UploadValues("http://localhost:57440/", "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(responsebytes);

                label3.Text=responsebody;
            }
        }
                
        private void button2_Click(object sender, EventArgs e)
        {
        }

        private static int InstallService()
        {
            

            return 0;
        }

        private static int UninstallService()
        {
          

            return 0;
        }
    }
}
