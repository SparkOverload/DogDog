using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DogDog_WEB.Class
{
    public class DataStore
    {
        // ===== URL ======
        public static readonly string DOG_DOG_API_URL = ConfigurationManager.AppSettings["DOG_DOG_API_URL"];
        // ===== API_Server ======
        internal static readonly string SignIn = DOG_DOG_API_URL + "api/LoginSubmit/";
        internal static readonly string SignOut = DOG_DOG_API_URL + "api/LogoutSubmit/";
        internal static readonly string SignUp = DOG_DOG_API_URL + "api/ProfileSubmit/";
        internal static readonly string RequestData = DOG_DOG_API_URL + "api/RequestData/";


        // ===== Cookie ======
        public static readonly string COOKIE_UID = "ID_USER";
        public static readonly string COOKIE_UDATA = "DATA_INFO";


        internal static bool validateCookie(HttpRequestBase Request)
        {
            if (Request.Cookies[DataStore.COOKIE_UID] == null || Request.Cookies[DataStore.COOKIE_UDATA] == null)
            {
                return false;
            }

            return true;
        }
    }
}