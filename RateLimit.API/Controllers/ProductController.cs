using Microsoft.AspNetCore.Mvc;

namespace RateLimit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetProduct() 
        {
            return Ok(new { Id = 1, Name = "Pencil", Price = 20 });
        }

        [HttpPost]
        public ActionResult AddProduct()
        {
            return Ok(new { Status = "Success" });
        }
    }
}