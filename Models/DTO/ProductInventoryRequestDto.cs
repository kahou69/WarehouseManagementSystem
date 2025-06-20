using System.ComponentModel.DataAnnotations;

namespace WarehouseManagament.Models.DTO
{
    public class ProductInventoryRequestDto
    {
        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Storage Location ID is required.")]
        public int StorageLocationId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }




    }
}
