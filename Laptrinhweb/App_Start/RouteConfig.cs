using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Laptrinhweb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product",
                url: "ABC/All",
                defaults: new { controller = "ABC", action = "All", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Signin",
                url: "Man/signin",
                defaults: new { controller = "Man", action = "signin", id = UrlParameter.Optional }
            );

            

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ABC", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
