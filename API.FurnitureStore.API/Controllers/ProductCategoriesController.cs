using Microsoft.AspNetCore.Http;
using API.FurnitureStore.Data;
using API.FurnitureStore.Shared;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;

namespace API.FurnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly ApiFurnitureStoreContext _context;
        public ProductCategoriesController(ApiFurnitureStoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<ProductCategory>> Get()
        {
             return await _context.ProductCategories.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var productCategory = await _context.ProductCategories.FirstOrDefaultAsync(p => p.Id == id);
            if (productCategory == null) return NotFound();
            return Ok(productCategory);
        }
        [HttpPost]
        public async Task<IActionResult> Post(ProductCategory category)
        {
            await _context.ProductCategories.AddAsync(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Post",category.Id, category);
        }
        [HttpPut]
        public async Task<IActionResult> Put(ProductCategory category)
        {
            _context.ProductCategories.Update(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult>Delete(ProductCategory category)
        {
            if(category == null) return NotFound();
            _context.ProductCategories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
