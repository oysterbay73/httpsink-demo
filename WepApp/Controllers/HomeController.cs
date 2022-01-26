using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WepApp.Models;

namespace WepApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var i = 1;
            while (i <= 10)
            {
                _logger.LogInformation("Sending web app {index}", i);
                i++;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            var i = 1;
            while (i <= 10)
            {
                _logger.LogInformation("Sending web app privacy {index}", i);
                i++;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}