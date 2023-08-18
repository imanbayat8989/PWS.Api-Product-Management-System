using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PWS.Api.Data;
using PWS.Api.Models;

namespace PWS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PWSDbContext _PwsDbContext;
        public ProductsController(PWSDbContext pWSDbContext)
        {
            this ._PwsDbContext = pWSDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _PwsDbContext.Products.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody]Product product)
        {
            product.Id = Guid.NewGuid();

            _PwsDbContext.Products.AddAsync(product);
            await _PwsDbContext.SaveChangesAsync();

            return Ok(product);
        }
    }
}
