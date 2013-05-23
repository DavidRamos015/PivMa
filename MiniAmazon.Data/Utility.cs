using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniAmazon.Data
{
    public class FacebookUtility
    {
        public static string SessionTokenName = "AccessToken";
        public static string Token = "CAACqAT0uM6QBADvZBFie5W2gcTM9BBkP6LAczZA5scUP3xwrYxRlvzbHtaINClRiZBISZCjqsqYhxSrRlRimlxeBpo3c6xXpeMigrcqHdPb2J5LZBlaZARkHt5hGlPVZCxqfoZAFVYZCh1cda3tOIqW71U4xC9POWR6R0neS3WfalGgZDZD";
        public static string UserId = "1140518528";
        public static string ApplicationID = "186922298127268";
        public static string ApplicationSecretCode = "6372e39bee03e83b916e277e189bc7e2";
        public static string ApplicationNamespace = "pivmafblogin";

        public static string GenerateUrlProduct(string ProductId)
        {
            string Url = Utility.UrlAplication + "/DashBoard/ProductDetail/" + ProductId;
            return Url;
        }
    }

    public class Utility
    {

        public static bool RemoteConnection = false;
        public static string MsjNeedToLogin = "Necesitas iniciar sesión para utilizar esta sección,";
        public static string AdminRole = "Admin";
        public static string UserRole = "User";
        public static string AdminEmail = "mformytest@gmail.com";
        public static string AdminPassWord = "pwmformytest123";
        public static string AdminName = "Administrador";

        public static string AplicationName =  "miniamazon2";
        public static string UrlAplication = "http://" + AplicationName + ".apphb.com";
        public static string UrlToConfirm = "http://" + AplicationName + "/Confirmations/Create_Record";


        public static string ConnectionString
        {
            get
            {
                if (RemoteConnection)
                    return "MiniAmazon.Remote";
                else
                    return "MiniAmazon.Local";
            }
        }

    }
}
