using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace WarehouseManagament.Models
{
    public enum StorageLocationType
    {
        Bin = 0,
        Shelf = 1,
        Pallet = 2,
        Rack = 3
    }

    public class StorageLocation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Location code cannot be longer than 50 characters.")]
        public String LocationCode { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StorageLocationType LocationType { get; set; }
        public int Capacity { get; set; }
        public int OccupiedCapacity { get; set; }

        public int AvailableCapacity => Capacity - OccupiedCapacity;

        //hello
    }
}
