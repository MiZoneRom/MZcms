using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace MZcms.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { Controller = "Test", Action = "Get", id = RouteParameter.Optional, defaultnamespace = "api" }
            );
            config.Routes.MapHttpRoute(
                name: "SmallApi",
                routeTemplate: "SmallProgAPI/{controller}/{action}/{id}",
                defaults: new { Controller = "Test", Action = "Get", id = RouteParameter.Optional, defaultnamespace = "SmallProgAPI" }
            );
            //config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));


        }
    }
}
