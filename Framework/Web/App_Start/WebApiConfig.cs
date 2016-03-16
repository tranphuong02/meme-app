using System.Web.Http;
using Framework.Utility.WebApiHelper;

namespace Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var formatters = GlobalConfiguration.Configuration.Formatters;
            JsonFormatter.CamelCasePropertyNamesContractResolver(formatters);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}/{id}", new { action = "Index", id = RouteParameter.Optional });
        }
    }
}