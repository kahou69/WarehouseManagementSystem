using WarehouseManagament.Models.DTO;

namespace WarehouseManagament.Models.Mappers
{
    public class ProductDtoMapper : IDtoMapper<Product, ProductDto>
    {
        public ProductDto ToDto(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Product entity cannot be null.");
            }
            return new ProductDto
            {
                Name = entity.Name,
                Price = entity.Price
            };
        }
        public Product ToEntity(ProductDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto), "ProductDto cannot be null.");
            }
            return new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };
        }
    }
 
}
