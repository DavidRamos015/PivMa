using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;

using System.Web.Optimization;



namespace MiniAmazon.Web
{
    public class RouteConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("SignIn", "SignIn", new { controller = "MyAccount", action = "SignIn" });


            routes.MapRoute("Error", "Error", new { controller = "Errors", action = "Error404" });

            routes.MapHttpRoute(
                 name: "FacebookApi",
                 routeTemplate: "{controller}/{id}/api",
                 defaults: new { id = RouteParameter.Optional }
             );

           // routes.MapRoute(
           //    "Usuarios_default",
           //    "Usuarios/{controller}/{action}/{id}",
           //    new { action = "Index", id = UrlParameter.Optional }
           //);

        //    routes.MapHttpRoute(
        //    name: "DefaultApi",
        //    routeTemplate: "api/{controller}/{id}",
        //    defaults: new { id = RouteParameter.Optional }
        //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "DashBoard", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}