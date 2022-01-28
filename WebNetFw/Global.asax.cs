using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebNetFw
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Log.Logger = GetLogger();
        }

        private static Serilog.Core.Logger GetLogger()
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.DurableHttpUsingFileSizeRolledBuffers(
                    requestUri: "https://www.mylogs.com",
                    httpClient: null,
                    bufferBaseFileName: HostingEnvironment.MapPath("/buffer"), // use map path or absolute path otherwise logs to location of iis worker process (if running under iis)
                    bufferFileShared: true) // required for multi threaded app - ensure app pool has full control
                .CreateLogger();
        }
    }    
}
