using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using UserCenter.IServices;
using UserCenter.OpenAPI.Filters;

namespace UserCenter.OpenAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            InitAutofac();

            GlobalConfiguration.Configure(WebApiConfig.Register);
          
        }
        private void InitAutofac()
        {
            var configuration = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterWebApiFilterProvider(configuration);
            //一个对象必须是IOC容器创建出来的，IOC容器才会帮我们自动注入
            builder.RegisterType(typeof(BasicAuthorizationFilter)).PropertiesAutowired();

            var services = Assembly.Load("UserCenter.Services");
            builder.RegisterAssemblyTypes(services)
                .Where(type=>!type.IsAbstract&&typeof(IServiceTag).IsAssignableFrom(type))
                .AsImplementedInterfaces()
                .SingleInstance()
                .PropertiesAutowired();
            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;
        }
    }
}
