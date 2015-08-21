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
        static readonly string PasswordHash = "joaquimcoveiro";
        static readonly string SaltKey = "kimroscas";
        static readonly string VIKey = "dddns.pt-secret!";
        static string userdomain = "joaquimcoveiro.dddns.pt";
        // hash para os dados acima
        string hash = "vmS9bEB9E6bPmgMPBnB1bsrvlotDIHatHW0x9xvqeeuwAtBjwJC5tvQUnFltXg0f";

        // ao registar o serviço o utilizador só precisa de fornecer a HASH gerada
        // no momento do registo no site
        // cada envio é: PUBLICip + hash

        public DecimalDNSService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Library.WriteErrorLog("Service started.");

            try
            {
                System.Diagnostics.EventLog.WriteEntry("DecimalDNSService", "DecimalDNSService started working.", EventLogEntryType.Information, 19282);
            }

            catch (Exception exp)
            {
                Library.WriteErrorLog("! Erro no eventlog:" + exp.Message);
            }

            Library.WriteErrorLog("Service started. Eventlog ok.");

            timer1 = new Timer();
            timer1.Interval = 15000;
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_tick);
            timer1.Enabled = true;
            Library.WriteErrorLog("Timer active.");
            timer1_tick(null, null);
        }

        private void timer1_tick(object sender, ElapsedEventArgs e)
        {
            Library.WriteErrorLog("Tick in.");

            string t = "";
            string GetPublicIPURL = "http://decimal.pt/get.php"; // decimal.pt/get.php

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
                chave = hash; // se for preciso encriptar => Encrypt(PasswordHash + "+" + SaltKey + "+" + userdomain);
                Library.WriteErrorLog(publicIP + " " + chave.ToString());
                //Library.WriteErrorLog(Decrypt(chave));
            }
            catch (Exception exp)
            {
                Library.WriteErrorLog(exp.Message);
            }

            Library.WriteErrorLog(t);
            System.Diagnostics.EventLog.WriteEntry("DecimalDNSService", t + " hash=" + chave, EventLogEntryType.Information, 19283);

            Library.WriteErrorLog("Tick out.");

        }

        protected override void OnStop()
        {
            timer1.Enabled = false;
            Library.WriteErrorLog("Service stopped.");
        }

        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
    }
}
