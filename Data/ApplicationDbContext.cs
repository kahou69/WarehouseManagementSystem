using Microsoft.EntityFrameworkCore;
using WarehouseManagament.Models;

namespace WarehouseManagament.Data
{
    public class ApplicationDbContext : DbContext // Fix CS0311: Ensure ApplicationDbContext inherits from DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }


        // Define DbSets for your entities (tables in the database)
        public DbSet<Product> Products { get; set; }
        public DbSet<StorageLocation> StorageLocations { get; set; }    
        public DbSet<ProductInventory> ProductInventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StorageLocation>()
                .HasIndex(sl => sl.LocationCode)
                .IsUnique();

            // Configure LocationType to be stored as string
            modelBuilder.Entity<StorageLocation>()
                .Property(sl => sl.LocationType)
                .HasConversion(
                    v => v.ToString(),    // Convert enum to string for storage
                    v => (StorageLocationType)Enum.Parse(typeof(StorageLocationType), v)  // Convert string back to enum when reading
                );
        }
    }
}
