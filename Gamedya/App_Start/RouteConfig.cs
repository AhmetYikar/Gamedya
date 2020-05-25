using Gamedya.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gamedya
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.Add("NewsDetails", new SeoFriendlyRoute("news/details/{id}",
            new RouteValueDictionary(new { controller = "news", action = "Details" }),
            new MvcRouteHandler()));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
