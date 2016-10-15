using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Configuration;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web.Http.Dispatcher;

namespace WebAPITesting
{
    public static class WebApiConfig
    {
		public static string dbConnectionString;
        public static string apiUrl;
        public static void Register(HttpConfiguration config)
        {
            // this configuration indicates that routes will be taken care of via route attributes in the controller methods
            config.MapHttpAttributeRoutes();

            // no longer the below is necessary if you use attributes in your routing
            // wep api will be able to find the controller and the given route based on the attribute on the method
            //config.Routes.MapHttpRoute(
            //    name: "RegisterAppAPI",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional, controller = "RegisterData" }
            //);

            // the following removes the need to append the 'Controller' suffix to all of your web api controllers
            // it essentially overrides the web api hard-coded requirement that the controller "HasValidControllerName"
            // Hard-coded, default requirements for web api controllers include:
            // type != null
            // type.IsClass
            // type.IsVisible
            // !type.IsAbstract
            // typeof(IHttpController).IsAssignableFrom(type) - since ApiController is based on IHttpController, ApiController meets this requirement
            // HasValidControllerName(type) - this is the hard-coded requirement that causes the need for the 'Controller' suffix and
            // what is overridden below, essentially setting the ControllerSuffix field to an empty string
            var suffix = typeof(DefaultHttpControllerSelector).GetField("ControllerSuffix", BindingFlags.Static | BindingFlags.Public);
            if (suffix != null) suffix.SetValue(null, string.Empty);

            dbConnectionString = ConfigurationManager.ConnectionStrings["AccountRegisterApp"].ConnectionString;
            apiUrl = ConfigurationManager.AppSettings["RegisterDataAPIUrl"];

        }
    }
}
