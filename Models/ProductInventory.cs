namespace WarehouseManagament.Models
{
    public class ProductInventory
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int StorageLocationId { get; set; }
        public StorageLocation StorageLocation { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
