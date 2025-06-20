using WarehouseManagament.Models.DTO;

namespace WarehouseManagament.Models.Mappers
{
    public class ProductInventoryRequestDtoMapper : IDtoMapper<ProductInventory, ProductInventoryRequestDto>
    {
        public ProductInventoryRequestDto ToDto(ProductInventory entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "ProductInventory entity cannot be null.");
            }
            return new ProductInventoryRequestDto
            {
                ProductId = entity.ProductId,
                StorageLocationId = entity.StorageLocationId,
                Quantity = entity.Quantity
            };
        }
        public ProductInventory ToEntity(ProductInventoryRequestDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "ProductInventoryRequestDto cannot be null.");
            }
            return new ProductInventory
            {
                ProductId = dto.ProductId,
                StorageLocationId = dto.StorageLocationId,
                Quantity = dto.Quantity
            };
        }
    }
}
