using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using DistributedServices.BoundedContext.Main.API.Resolver;

namespace DistributedServices.BoundedContext.Main.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            config.DependencyResolver = new UnityResolver(container);

            Bootstraper bootstraper = new Bootstraper(container);
            bootstraper.Initialize();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
