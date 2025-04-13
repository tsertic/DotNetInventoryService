using InventoryApi.Models;
using Microsoft.EntityFrameworkCore;


namespace InventoryApi.Data
{
    public class InventoryDbContext:DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options):base(options) { }

        public DbSet<InventoryItem> InventoryItems { get; set; } = null!;
    }
}
