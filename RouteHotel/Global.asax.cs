using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace RouteHotel
{
    public class Global : System.Web.HttpApplication
    {
        
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            SessionObjects.CreateNewSessionObject();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // see http://forums.asp.net/t/1687650.aspx?On+Session_End+Event+SessionId+is+null+
            /* Instead using the  HttpContext.Current.Session.SessionID. You should use Application.Session.SessionID in Session_End event.

                Because in this period, HttpContext.Current is null, there's no request and response from the user.

                void Session_End(object sender, EventArgs e)
                {
                    string sessionId = this.Session.SessionID;

                    //your code...
                }
             */
            SessionObjects.DeleteSessionObject(this.Session.SessionID);
        }


        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}