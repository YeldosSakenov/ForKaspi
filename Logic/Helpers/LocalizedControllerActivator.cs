using Core.Helpers;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Domain.Logic.Helpers
{
    public class LocalizedControllerActivator : IControllerActivator
    {
        private string _DefaultLanguage = "en";

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            object lang = requestContext.RouteData.Values["lang"] ?? _DefaultLanguage;

            if ((string)lang != _DefaultLanguage)
            {
                try
                {
                    Thread.CurrentThread.CurrentCulture =
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo((string)lang);

                    HttpContext context = HttpContext.Current;
                    context.Session["lang"] = lang;
                }
                catch
                {
                    throw new NotSupportedException(String.Format("ERROR: Invalid language code '{0}'.", lang));
                }
            }

            SiteSession.CurrentUICulture = lang.ToString();

            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}
