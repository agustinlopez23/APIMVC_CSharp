using API.FurnitureStore.Data;
using API.FurnitureStore.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.FurnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApiFurnitureStoreContext _context;
        public ProductsController(ApiFurnitureStoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var product = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("GetByCategory/{productCategoryId}")]
        public async Task<IEnumerable<Product>>GetByCategory(int productCategoryId)
        {
            return await _context.Products.Where(p=>p.ProductCategoryId==productCategoryId).ToListAsync();
        }
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Post", product.Id, product);

        }
        [HttpPut]
        public async Task<IActionResult>Put(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Product product)
        {
            if(product==null) return NotFound();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
