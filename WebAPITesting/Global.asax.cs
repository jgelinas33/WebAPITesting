using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebAPITesting
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // the following clears response formatters (default is xml)
            // and forces responses to be in JSON format no matter what the request indicates it wants in the Accept portion of the header
            // however, setting this/clearing the formatters requires that only json type data be incoming from the client
            // name/value pairs etc... will fail
            //GlobalConfiguration.Configuration.Formatters.Clear();
            //GlobalConfiguration.Configuration.Formatters.Add(new JsonMediaTypeFormatter());
        }
    }
}
