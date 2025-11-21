using LoggingNLogLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoggingNLogLibrary.Controllers
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
            _logger.LogTrace("Index page started");
            _logger.LogDebug("Index page started");
            _logger.LogInformation("Index page started");
            _logger.LogWarning("Index page started");
            _logger.LogError("Index page started");
            _logger.LogCritical("Index page started");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
