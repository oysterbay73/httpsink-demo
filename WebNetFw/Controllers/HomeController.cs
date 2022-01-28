using Serilog;
using System.Diagnostics;
using System.Web.Mvc;

namespace WebNetFw.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {           
            var i = 1;
            while (i <= 10)
            {
                Log.Logger.Information("Sending home page {index}", i);
                i++;
            }           

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

            var i = 1;
            while (i <= 10)
            {
                Log.Logger.Information("Sending home page contact {index}", i);
                i++;
            }

            return View();
        }

        
    }
}