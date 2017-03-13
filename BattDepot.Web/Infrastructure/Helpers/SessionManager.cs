using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Threading;
using System.Globalization;

namespace MultiLanguage
{
    public class SessionManager
    {
        protected HttpSessionState session;

        public SessionManager(HttpSessionState httpSessionState)
        {
            session = httpSessionState;
        }

        public static string CurrentCulture
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture.Name;
            }
            set
            {
                if(value!=null)
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(value);
                else
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

                //Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            }
        }
    }
}