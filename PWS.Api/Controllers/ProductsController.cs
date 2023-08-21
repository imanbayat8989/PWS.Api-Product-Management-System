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
            this._PwsDbContext = pWSDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _PwsDbContext.Products.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            product.Id = Guid.NewGuid();

            _PwsDbContext.Products.AddAsync(product);
            await _PwsDbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _PwsDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, Product updateProductRequest)
        {
            var product = await _PwsDbContext.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            product.Name = updateProductRequest.Name;
            product.Type = updateProductRequest.Type;
            product.Color = updateProductRequest.Color;
            product.Price = updateProductRequest.Price;

            await _PwsDbContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _PwsDbContext.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            _PwsDbContext.Products.Remove(product);
            await _PwsDbContext.SaveChangesAsync();
            return Ok(product);
        }
    }
}
