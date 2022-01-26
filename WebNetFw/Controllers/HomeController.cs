using Serilog;
using System.Diagnostics;
using System.Web.Mvc;

namespace WebNetFw.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            

            Log.Logger = GetLogger(); // static version
            var i = 1;
            while (i <= 10)
            {
                Log.Logger.Information("Sending home page {index}", i);
                i++;
            }

            //using (var logger = GetLogger()) // disposed local version
            //{
            //    var i = 1;
            //    while (i <= 10)
            //    {
            //        logger.Information("Sending home page {index}", i);
            //        i++;
            //    }

            //    Thread.Sleep(2100); // seems to hang if we don't sleep before flushing
            //}

            return View();
        }       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            Log.Logger = GetLogger(); // static version
            var i = 1;
            while (i <= 10)
            {
                Log.Logger.Information("Sending home page contact {index}", i);
                i++;
            }

            return View();
        }

        private Serilog.Core.Logger GetLogger()
        {
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.DurableHttpUsingFileSizeRolledBuffers(
                    requestUri: "https://www.mylogs.com",
                    httpClient: null,
                    bufferBaseFileName: HttpContext.Server.MapPath("buffer"), // use map path or absolute path otherwise logs to location of iis worker process (if running under iis)
                    bufferFileShared: true) // required for multi threaded app - ensure app pool has full control
                .CreateLogger();
        }
    }
}