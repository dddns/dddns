// this .cs is shared between projects
// belongs in the DecimalDNSService but it is linked in the others
// add - existing - select - add as link

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing;
using System.Linq;
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
    class tools
    {
        // gloval variables to be used across several .cs

        // hash - comes from settings.xml and it is used
        // to POST to server for IP update on server
        // this is the "secret" to update the client IP
        public static string hash = "";

        // logtofile - use 1 to log to file, other string won't log
        public static string logtofile = "1";

        // logtoEV - use 1 to log to eventviewer, other string won't log
        public static string logtoEV = "1";

        // updateinterval - miliseconds interval for the client to
        // update the IP on the server
        // default is 5 minutes (5 times 60 times 1000)
        // but updateinterval is read from settings.xml too
        public static int updateinterval = 5 * 60 * 1000;

        // server that will receive the IP+hash
        public static string serverurl = "";

        // php page that will give the public IP
        public static string publicip = "";

        public static void GetXMLSettings()
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
                            WriteErrorLog("logtofile ok");
                            break;
                        // logtoeventviewer should be second to load
                        case "logtoeventviewer":
                            tools.logtoEV = reader.ReadString();
                            WriteErrorLog("logtoEV ok");
                            break;
                        case "hash":
                            tools.hash = reader.ReadString();
                            WriteErrorLog("hash ok");
                            break;
                        case "updateinterval":
                            tools.updateinterval = Convert.ToInt32(reader.ReadString());
                            WriteErrorLog("updateinterval ok");
                            break;
                        case "serverurl":
                            tools.serverurl = reader.ReadString();
                            WriteErrorLog("serverurl ok");
                            break;
                        case "publicip":
                            tools.publicip = reader.ReadString();
                            WriteErrorLog("publicip ok");
                            break;
                    }
                }
            }
            catch (Exception exp)
            {
                tools.hash = "";
                WriteErrorLog("XML: " + exp.Message);
            }
            if (0 == xmlfound)
            {
                System.Diagnostics.EventLog.WriteEntry("DecimalDNSService", "ERROR! hash not found.", EventLogEntryType.Error, 19289);
                WriteErrorLog("Error in settings.xml.");
            }

        }

        public static void WriteErrorLog(Exception ex)
        {
            if ("1" != tools.logtofile) return;

            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + " " + ex.Source.ToString().Trim() + " " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        public static void WriteErrorLog(string Message)
        {
            if ("1" != tools.logtofile) return;

            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @"\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
    }
}
