using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace assessment_platform_developer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable attribute routing.
            config.MapHttpAttributeRoutes();

            // Default API route.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}