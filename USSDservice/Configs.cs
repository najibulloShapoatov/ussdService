using System.Configuration;

namespace BabilonUSSD
{
    class Configs
    {
        public static string Gateway = ConfigurationManager.AppSettings["SMSC_Gateway_IP"];
        public static int Port = int.Parse(ConfigurationManager.AppSettings["SMSC_Gateway_Port"]);
        public static string Username = ConfigurationManager.AppSettings["SMSC_Gateway_Username"];
        public static string Password = ConfigurationManager.AppSettings["SMSC_Gateway_Password"];
        public static string Service = ConfigurationManager.AppSettings["SMSC_Gateway_ServiceNumber"];
        public static string NotifAddress = ConfigurationManager.AppSettings["Notif_Address"];
        public static bool EchoInput = bool.Parse(ConfigurationManager.AppSettings["EchoInput"]);
        public static bool UssdMenuMode = bool.Parse(ConfigurationManager.AppSettings["UssdMenuMode"]);
        public static string SqlConnect = ConfigurationManager.AppSettings["SQL_Connect"];
    }
}