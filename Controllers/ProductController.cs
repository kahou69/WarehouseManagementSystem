using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManagament.Data;
using WarehouseManagament.Models;
using WarehouseManagament.Models.DTO;
using WarehouseManagament.Models.Mappers;

namespace WarehouseManagament.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> addProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product cannot be null.");
            }
             var product = new ProductDtoMapper().ToEntity(productDto);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductId), new { id = product.Id }, product);
        }

        [HttpGet]
        public async Task<IActionResult> getAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductId(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(product);
        }

        [HttpGet("search/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Product name cannot be empty.");
            }
            
            var products = await _context.Products.Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();

            if (products.Count == 0)
            {
                return NotFound($"product with the name {name} does not exists");
            }

            return Ok(products);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto updatedProductDto)
        {
            if (updatedProductDto == null)
            {
                return BadRequest("Invalid data entered.");
            }

            var existingProduct = await _context.Products.FindAsync(id);    
            if (existingProduct == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            existingProduct.Price = updatedProductDto.Price;
            existingProduct.Name = updatedProductDto.Name;

            await _context.SaveChangesAsync();
            return Ok(existingProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }   
    }
}
