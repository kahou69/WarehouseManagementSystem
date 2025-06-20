using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WarehouseManagament.Models.DTO
{
    public class StorageLocationRequestDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "Location code cannot be longer than 50 characters.")]
        public String LocationCode { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StorageLocationType LocationType { get; set; }
        public int Capacity { get; set; }
    }
}
