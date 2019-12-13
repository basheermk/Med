using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Medacademy.Repository
{
    public static class SessionClass
    {       
        
        public static string SessionId()
        {
            string session = HttpContext.Current.Session.SessionID;
            return session;
        }
        public enum Users
        {
            Admin = 1,
            User =2
        }
    }
}