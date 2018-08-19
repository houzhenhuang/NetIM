using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UserCenter.OpenAPI.Filters;

namespace UserCenter.OpenAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Filters.Add(new BasicAuthorizationFilter());
            //一个对象必须是IOC容器创建出来的，IOC容器才会帮我们自动注入(在BasicAuthorizationFilter中想要属性注入的AppInfoService，原理就是如此)
            //先将这个过滤器注入到IOC，然后由IOC创建出来给我们，我们添加到 config.Filters中
            //BasicAuthorizationFilter authorFilter =(BasicAuthorizationFilter)config.Services.GetService(typeof(BasicAuthorizationFilter));
            BasicAuthorizationFilter authorFilter = (BasicAuthorizationFilter)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(BasicAuthorizationFilter));
            config.Filters.Add(authorFilter);
        }
    }
}
