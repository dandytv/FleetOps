using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CardTrend.Common.Extensions;
using System.Threading.Tasks;
using System.Xml.Linq;
using CardTrend.Common.EncryptData;
using System.Configuration;
using NLog;
using System.Security.Cryptography;

namespace CardTrend.Common.Helpers
{
    public static class AppConfigurationHelper
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static string CCMSCnnStr = null;
        public static string CCMSEntityWebCnnStr
        {
            get
            {
                if (CCMSCnnStr == null)
                {
                    CCMSCnnStr = Encryption.Decrypt(ConfigurationManager.ConnectionStrings["pdb_ccmsEntityWebContext"].ConnectionString);
                }
                return CCMSCnnStr;
            }
        }

        private static string ccmsCnnStr = null;
        public static string pdb_ccmsCnnStr
        {
            get
            {
                if (ccmsCnnStr == null)
                {
                    ccmsCnnStr = Encryption.Decrypt(ConfigurationManager.ConnectionStrings["pdb_ccmsContext"].ConnectionString);
                }
                return ccmsCnnStr;
            }
        }
        public static string PasswordGenerator()
        {
            const string allowedCharAndNumbers = "abcdefghijkmnopqrstuvwxyz0123456789";
            char[] chars = new char[6];
            Random rd = new Random();

            for (int i = 0; i < 6; i++)
            {

                chars[i] = allowedCharAndNumbers[rd.Next(0, allowedCharAndNumbers.Length - 1)];
            }

            return new string(chars);
        }
        public static string AutoHashing(string password)
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(password);
            byte[] hashValue = mySHA256.ComputeHash(bytes);
            int i;
            string tempHash = "";
            for (i = 0; i < hashValue.Length; i++)
            {
                tempHash += String.Format("{0:X2}", hashValue[i]);
                if ((i % 4) == 3) tempHash += " ";
            }
            return tempHash;
        }
        public static void EncryptionConnectionString(string ConnectionString)
        {
            if (!Encryption.IsEncrypt(ConnectionString))
            {
                try
                {
                    var hasChange = false;
                    string cnnStrFile;
                    // This will get called on startup
                    var stream = new FileStream(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, FileMode.Open, FileAccess.Read);

                    // encrypt SharePassword  
                    XDocument xDoc = XDocument.Load(stream);
                    stream.Close();

                    // encrypt the connection string
                    // find connection string file
                    cnnStrFile = xDoc.Root.Element("connectionStrings").Attribute("configSource").Value;
                    string currentDir = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                    cnnStrFile = Path.Combine(currentDir, cnnStrFile);
                    stream = new FileStream(cnnStrFile, FileMode.Open, FileAccess.Read);

                    xDoc = XDocument.Load(stream);
                    stream.Close();
                    xDoc.Root.Elements("add").ForEach(e =>
                    {
                        // remove CCMSEntity & CCMSEntityWeb connectionString without encrypting temporary
                        if (!Encryption.IsEncrypt(e.Attribute("connectionString").Value) && e.Attribute("name").Value != "CCMSEntity" && e.Attribute("name").Value != "CCMSEntityWeb")
                        {
                            e.SetAttributeValue("connectionString", Encryption.Encrypt(e.Attribute("connectionString").Value));
                            hasChange = true;
                        }
                    });
                    if (hasChange)
                    {
                        xDoc.Save(cnnStrFile);
                        ConfigurationManager.RefreshSection("appSettings");
                        ConfigurationManager.RefreshSection("connectionStrings");
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
        }
    }
}
