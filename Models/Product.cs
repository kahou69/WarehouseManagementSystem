using System.Text.Json.Serialization;

namespace WarehouseManagament.Models
{
    public class Product
    {
        public int Id { get; set; }

        // null! indicates to the compiler "i know this property is assigned to null, but i am explicitly assuring that
        // it will never be null when used

        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
