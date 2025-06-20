using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManagament.Migrations
{
    /// <inheritdoc />
    public partial class ConvertLocationTypEnumToStoreString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LocationType",
                table: "StorageLocations",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LocationType",
                table: "StorageLocations",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
