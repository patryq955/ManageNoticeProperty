using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ManageNoticeProperty
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Property",
                url: "Nieruchomość-{id}",
                defaults: new { controller = "Property", action = "GetProperty" }
            );

            routes.MapRoute(
                name: "EditProperty",
                url: "edycja-{id}",
                defaults: new { controller = "Property", action = "EditProperty" }
            );

            routes.MapRoute(
                name: "Message",
                url: "MessageInfo-{id}",
                defaults: new { controller = "Message", action = "MessageInfo" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
