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

        //GET api/product/pencil
        [HttpGet("{name}")]
        public ActionResult GetProduct(string name)
        {
            return Ok(name);
        }

        [HttpPost]
        public ActionResult AddProduct()
        {
            return Ok(new { Status = "Success" });
        }

        [HttpPut]
        public ActionResult UpdateProduct() 
        {
            return Ok(new { Status = "Success" });
        }
    }
}