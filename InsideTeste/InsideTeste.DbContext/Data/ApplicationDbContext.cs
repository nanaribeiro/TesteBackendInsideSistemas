using InsideTeste.Database.Models;
using Microsoft.EntityFrameworkCore;
namespace InsideTeste.Database.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<OrderItem> Items { get; set; } = default!;
    }
}
