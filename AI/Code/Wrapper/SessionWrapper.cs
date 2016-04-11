using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AI.Models;
using System.Web.SessionState;

namespace AI.Code.Wrapper
{
    public class SessionWrapper
    {
        private SessionWrapper()
        {
        }

        private static SessionWrapper instance;
        public static SessionWrapper Instance
        {
            get
            {
                return (instance ?? (instance = new SessionWrapper()));
            }
        }

        public void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        public SessionUser CurrentUser
        {
            get
            {
                return HttpContext.Current.Session["CurrentUser"] as SessionUser;
            }
            set
            {
                HttpContext.Current.Session["CurrentUser"] = value;
            }
        }
    }
}