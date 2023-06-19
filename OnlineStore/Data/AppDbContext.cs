using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductMvc.Models;

namespace ProductMvc.Data
{
    public class AppDbContext:IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

    }
}
