using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Text.RegularExpressions;
using System.Xml;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;
using System.Security;
using System.Security.Cryptography;
using System.IO;

namespace DecimalDNSService
{
    public partial class DecimalDNSService : ServiceBase
    {
        private Timer timer1 = null;

        // ao registar o serviço o utilizador só precisa de fornecer a HASH gerada
        // no momento do registo no site
        // cada envio é: PUBLICip + hash

        public DecimalDNSService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            GetXMLSettings();

            Library.WriteErrorLog("Service started.");

            try
            {
                System.Diagnostics.EventLog.WriteEntry("DecimalDNSService",
                    "DecimalDNSService started working.",
                    EventLogEntryType.Information,
                    19283);
            }
            catch (Exception exp)
            {
                Library.WriteErrorLog("Error writting to eventlog." + exp.Message);
            }

            timer1 = new Timer();
            timer1.Interval = tools.updateinterval;
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_tick);
            timer1.Enabled = true;
            timer1.Start();
            // force on start of service to update
            timer1_tick(null, null);

            Library.WriteErrorLog("OnStart ok.");
        }

        private void GetXMLSettings()
        {
            string xmlfile = @"settings.xml";
            int xmlfound = 0;

            try
            {
                XmlTextReader reader = new XmlTextReader(AppDomain.CurrentDomain.BaseDirectory + @"\\" + xmlfile);
                while (reader.Read())
                {
                    string n = reader.Name;
                    xmlfound = 1;
                    switch (n)
                    {
                        // logtofile should be the first to be read from the xml
                        // because if it is !=1 we should not log to file
                        case "logtofile":
                            tools.logtofile = reader.ReadString();
                            Library.WriteErrorLog("logtofile ok");
                            break;
                        // logtoeventviewer should be second to load
                        case "logtoeventviewer":
                            tools.logtoEV = reader.ReadString();
                            Library.WriteErrorLog("logtoEV ok");
                            break;
                        case "hash":
                            tools.hash = reader.ReadString();
                            Library.WriteErrorLog("hash ok");
                            break;
                        case "updateinterval":
                            tools.updateinterval = Convert.ToInt32(reader.ReadString());
                            Library.WriteErrorLog("updateinterval ok");
                            break;
                        case "serverurl":
                            tools.serverurl = reader.ReadString();
                            Library.WriteErrorLog("serverurl ok");
                            break;
                        case "publicip":
                            tools.publicip = reader.ReadString();
                            Library.WriteErrorLog("publicip ok");
                            break;
                    }
                }
            }
            catch (Exception exp)
            {
                tools.hash = "";
                Library.WriteErrorLog("XML: " + exp.Message);
            }
            if (0 == xmlfound)
            {
                System.Diagnostics.EventLog.WriteEntry("DecimalDNSService", "ERROR! hash not found.", EventLogEntryType.Error, 19289);
                Library.WriteErrorLog("Error in settings.xml.");
            }

        }

        private void timer1_tick(object sender, ElapsedEventArgs e)
        {
            Library.WriteErrorLog("Tick in.");

            string t = "";
            string GetPublicIPURL = tools.publicip; // "http://decimal.pt/get.php"; // decimal.pt/get.php

            //IPAddress noip;
            //StringBuilder str = new StringBuilder("");
            //string pattern = @"[^A-Za-z0-9 _.]";
            string pattern = @"[^0-9 _.]";
            //
            // get from decimal.pt/get.php my real public IP address
            //
            WebClient client = new WebClient();
            string ippublic = client.DownloadString(GetPublicIPURL);
            // clean it
            string publicIP = Regex.Replace(ippublic, pattern, "").TrimEnd();
            t = "PUBLIC IP is " + publicIP + " from " + GetPublicIPURL + "";

            string chave = "";
            try
            {
                chave = tools.hash; // se for preciso encriptar => Encrypt(PasswordHash + "+" + SaltKey + "+" + userdomain);
                Library.WriteErrorLog(publicIP + "+" + chave.ToString());
                //Library.WriteErrorLog(Decrypt(chave));
            }
            catch (Exception exp)
            {
                Library.WriteErrorLog(exp.Message);
            }

            Library.WriteErrorLog(t);
            System.Diagnostics.EventLog.WriteEntry("DecimalDNSService",
                t + " hash=" + publicIP + "+" + chave.ToString(),
                EventLogEntryType.Information, 
                19284);

            // POST
            // https://msdn.microsoft.com/en-us/library/456dfw4f%28v=vs.110%29.aspx

            //WebRequest request = WebRequest.Create("http://adelinoaraujo.com/post.php?hash=" + tools.hash);
            WebRequest request = WebRequest.Create(tools.serverurl + publicIP + "+" + chave.ToString());
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Library.WriteErrorLog(responseFromServer);
            reader.Close();
            response.Close();

            // END POST

            Library.WriteErrorLog("Tick out.");
        }

        protected override void OnStop()
        {
            timer1.Enabled = false;
            // no need to update eventviewer because it will
            // register the service stop
            Library.WriteErrorLog("Service stopped.");
        }


    }
}
