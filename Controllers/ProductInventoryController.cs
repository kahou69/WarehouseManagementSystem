using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManagament.Data;
using WarehouseManagament.Models;
using WarehouseManagament.Models.DTO;
using WarehouseManagament.Models.Mappers;

namespace WarehouseManagament.Controllers
{
    [ApiController]
    [Route("api/product-inventory")]
    public class ProductInventoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProductInventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> createProductInventory([FromBody] ProductInventoryRequestDto piRDto)
        {
            bool isValid = piRDto != null && piRDto.ProductId > 0 && piRDto.StorageLocationId > 0 && piRDto.Quantity >= 0;

            var product = await _context.Products.FindAsync(piRDto.ProductId);
            var storageLocation = await _context.StorageLocations.FindAsync(piRDto.StorageLocationId);

            if (!isValid || product == null || storageLocation == null)
            {
                if (product == null)
                {
                    return NotFound($"Product with ID {piRDto.ProductId} not found.");
                }

                if (storageLocation == null)
                {
                    return NotFound($"Storage Location with ID {piRDto.StorageLocationId} not found.");
                }

                return BadRequest("Invalid product inventory request.");
            }

            bool isStorageLocationCapacityValid = piRDto.Quantity <= storageLocation.AvailableCapacity;

            if (!isStorageLocationCapacityValid)
            {
                return BadRequest($"Storage location [{storageLocation.LocationCode}] {storageLocation.AvailableCapacity} exceeded by product quantity {piRDto.Quantity}.");
            }

            var existedProductInventory = await _context.ProductInventories
                .FirstOrDefaultAsync(pi => pi.ProductId == piRDto.ProductId && pi.StorageLocationId == piRDto.StorageLocationId);

            if (existedProductInventory != null)
            {   
                //update quantity for each entities
                product.Quantity += piRDto.Quantity;
                storageLocation.OccupiedCapacity += piRDto.Quantity;
                existedProductInventory.Quantity += piRDto.Quantity;

                //update all the entities
                _context.ProductInventories.Update(existedProductInventory);
                _context.Products.Update(product);
                _context.StorageLocations.Update(storageLocation);

                await _context.SaveChangesAsync();
                return Ok(existedProductInventory);
            }
            else
            {
                var productInventory = new ProductInventoryRequestDtoMapper().ToEntity(piRDto);
                //update quantity for each entities
                product.Quantity += piRDto.Quantity;
                storageLocation.OccupiedCapacity += piRDto.Quantity;

                //set the product and storage location for the new product inventory
                productInventory.Product = product;
                productInventory.StorageLocation = storageLocation;

                _context.ProductInventories.Add(productInventory);
                _context.Products.Update(product);
                _context.StorageLocations.Update(storageLocation);

                await _context.SaveChangesAsync();
                return Ok(productInventory);
            }
        }
    }
}
