using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.WebApi;
using King.FlightSearch.Web.Controllers;
using King.FlightSearch.Web.Infrastructure;

namespace King.FlightSearch.Web
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        private static IContainerProvider _containerProvider;

        public IContainerProvider ContainerProvider => _containerProvider;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            DI.RegisterServices(builder);
            builder.RegisterApiControllers(typeof(AirportsController).Assembly);

            var container = builder.Build();
            _containerProvider = new ContainerProvider(container);

            GlobalConfiguration.Configuration.DependencyResolver
                = new AutofacWebApiDependencyResolver(container);
        }
    }
}