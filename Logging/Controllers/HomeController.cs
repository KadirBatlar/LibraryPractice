using Logging.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Logging.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggerFactory _loggerFactory;

        public HomeController(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public IActionResult Index()
        {
            var _logger = _loggerFactory.CreateLogger("HomeController");

            _logger.LogTrace("Index");
            _logger.LogDebug("Index");
            _logger.LogInformation("Index");
            _logger.LogWarning("Index");
            _logger.LogError("Index");
            _logger.LogCritical("Index");

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