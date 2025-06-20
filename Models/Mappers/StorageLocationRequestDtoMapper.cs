using WarehouseManagament.Models.DTO;

namespace WarehouseManagament.Models.Mappers
{
    public class StorageLocationRequestDtoMapper : IDtoMapper<StorageLocation, StorageLocationRequestDto>
    {
        public StorageLocationRequestDto ToDto(StorageLocation entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "StorageLocation entity cannot be null.");
            }
            return new StorageLocationRequestDto
            {
                LocationCode = entity.LocationCode,
                LocationType = entity.LocationType,
                Capacity = entity.Capacity
            };
        }
        public StorageLocation ToEntity(StorageLocationRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "StorageLocationRequestDto cannot be null.");
            }
            return new StorageLocation
            {
                LocationCode = dto.LocationCode,
                LocationType = dto.LocationType,
                Capacity = dto.Capacity,
            };
        }
    }
}
