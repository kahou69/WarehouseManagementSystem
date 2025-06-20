using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseManagament.Data;
using WarehouseManagament.Models;
using WarehouseManagament.Models.DTO;
using WarehouseManagament.Models.Mappers;

namespace WarehouseManagament.Controllers
{
    [ApiController]
    [Route("/api/storage-locations")]
    public class StorageLocationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StorageLocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddStorageLocation([FromBody] StorageLocationRequestDto slDto)
        {
            if (slDto == null)
            {
                return BadRequest("Storage location cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(slDto.LocationCode))
            {
                return BadRequest("Location code is Required.");
            }

            if (slDto.Capacity <= 0)
            {
                return BadRequest("Capacity must be greater than zero.");
            }

            bool isLocationCodeExists = await _context.StorageLocations.AnyAsync(sls => sls.LocationCode.ToLower() == slDto.LocationCode.ToLower());

            if (isLocationCodeExists)
            {
                return Conflict($"Storage location with code {slDto.LocationCode} already exists.");
            }

            var sl = new StorageLocationRequestDtoMapper().ToEntity(slDto);

            _context.StorageLocations.Add(sl);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStorageLocationById), new { id = sl.Id }, sl);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStorageLocations()
        {
            var storageLocations = await _context.StorageLocations.ToListAsync();
            return Ok(storageLocations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStorageLocationById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid storage location ID.");
            }

            var storageLocation = await _context.StorageLocations.FindAsync(id);

            if (storageLocation == null)
            {
                return NotFound($"Storage Location with the id {id} does not exists");
            }

            return Ok(storageLocation);
        }


        [HttpGet("location-code/{locationCode}")]
        public async Task<IActionResult> GetStorageLocationByLocationCode (string locationCode)
        {
            if (string.IsNullOrWhiteSpace(locationCode))
            {
                return BadRequest("Location code cannot be empty.");
            }

            var storageLocations = await _context.StorageLocations
                .Where(sls => sls.LocationCode.ToLower().Contains(locationCode.ToLower())).ToListAsync();

            if (storageLocations.Any() == false)
            {
                return NotFound($"Storage Location with location code [{locationCode}] is not found.");
            }

            return Ok(storageLocations);
        }

        [HttpGet("location-type/{locationType}")]
        public async Task<IActionResult> GetStorageLocationByLocationType(StorageLocationType locationType)
        {
            var storageLocations = await _context.StorageLocations
                .Where(sls => sls.LocationType == locationType).ToListAsync();
            if (storageLocations.Any() == false)
            {
                return NotFound($"Storage Location with location type [{locationType}] is not found.");
            }

            return Ok(storageLocations);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateStorageLocation(int id, [FromBody] StorageLocationRequestDto storageLocationRDto)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid storage location ID.");
            }

            if (storageLocationRDto == null)
            {
                return BadRequest("Storage location cannot be null.");
            }   

            var existingStorageLocation = await _context.StorageLocations.FindAsync(id);

            if (existingStorageLocation == null)
            {
                return NotFound($"Storage Location with the id {id} does not exists");
            }

            if(existingStorageLocation.OccupiedCapacity >  storageLocationRDto.Capacity)
            {
                return BadRequest("New capacity cannot be less than the occupied capacity.");
            }

            bool isLocationCodeExists = await _context.StorageLocations.AnyAsync
                                        (sls => sls.LocationCode.ToLower() == storageLocationRDto.LocationCode.ToLower())
                                        && storageLocationRDto.LocationCode != existingStorageLocation.LocationCode;

            if (isLocationCodeExists)
            {
                return Conflict($"Storage location with code {storageLocationRDto.LocationCode} already exists.");
            }



            existingStorageLocation.LocationCode = storageLocationRDto.LocationCode;
            existingStorageLocation.LocationType = storageLocationRDto.LocationType;
            existingStorageLocation.Capacity = storageLocationRDto.Capacity;

            await _context.SaveChangesAsync();
            return Ok(existingStorageLocation);
        }

        public async Task<IActionResult> DeleteStorageLocation(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid storage location ID.");
            }
            var storageLocation = await _context.StorageLocations.FindAsync(id);
            if (storageLocation == null)
            {
                return NotFound($"Storage Location with the id {id} does not exists");
            }
            _context.StorageLocations.Remove(storageLocation);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
