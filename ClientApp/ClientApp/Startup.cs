﻿using System.Net;
using System.Web.Http;
using Owin;
using System.Web.Http.Tracing;
using Microsoft.AspNet.WebHooks.Controllers;

namespace ClientApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            var controllerType = typeof(WebHookReceiversController);

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                    "DefaultApi",
                    "api/{controller}/{id}",
                    new { id = RouteParameter.Optional }
                );

            config.InitializeReceiveCustomWebHooks();

            var traceWriter = config.EnableSystemDiagnosticsTracing();
            traceWriter.IsVerbose = true;
            traceWriter.MinimumLevel = TraceLevel.Error;

            appBuilder.UseWebApi(config);
        }
    }
}
