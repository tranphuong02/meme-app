using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Routing;
using App.App_Data;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Core.DI;
using Core.Logger;
using Newtonsoft.Json;

namespace App
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ILogger _logger;

        public MvcApplication()
        {
            _logger = new Logger();
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // AutoFac Register
            var container = DependencyRegister.BuildContainer(new List<string> { "Core"});
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            Response.Clear();

            // Log exception
            _logger.Error(exception);

            var httpException = exception as HttpException;

            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                Server.ClearError();
                Response.Redirect("~/PageNotFound/");
            }
        }
    }
}
