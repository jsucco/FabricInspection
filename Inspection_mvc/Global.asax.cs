using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Inspection_mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            string debugMode = ConfigurationManager.AppSettings["debugMode"];
            if (debugMode == "true")
            {
                GenericIdentity MyIdentity = new GenericIdentity("testUser");
                String[] groups = { "Admin" };
                HttpContext.Current.User = new GenericPrincipal(MyIdentity, groups);
                return;
            }
               
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
            {
                UnAuthorized();
                return;
            }
        
            try
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                if (ticket != null)
                {
                    string[] groups = {""};
                    HttpContext.Current.User = new GenericPrincipal(new FormsIdentity(ticket), groups);
                } else
                {
                    UnAuthorized();
                }
            } catch (Exception ex)
            {

                UnAuthorized();
            }
                        
        }

        protected void UnAuthorized()
        {
            string LoginAddress = ConfigurationManager.AppSettings["Login"];
            Response.Redirect(LoginAddress + "?returnUrl=" + HttpContext.Current.Request.Url.AbsoluteUri);
        }
    }
}
