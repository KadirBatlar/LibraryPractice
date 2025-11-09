using ErrorHandling.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ErrorHandling.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [CustomExceptionFilterAttributeHandler(ErrorPage = "Error1")]
        public IActionResult Index()
        {
            // get exception
            int a = 5;
            var result = a / 0;
            return View();
        }

        [CustomExceptionFilterAttributeHandler(ErrorPage = "Error2")]
        public IActionResult Privacy()
        {
            throw new FileNotFoundException();
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.path = exception.Path;
            ViewBag.message = exception.Error.Message;
            return View();
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Error1()
        {
            return View();
        }

        public IActionResult Error2()
        {
            return View();
        }
    }
}