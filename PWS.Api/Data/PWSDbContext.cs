using Microsoft.EntityFrameworkCore;
using PWS.Api.Models;

namespace PWS.Api.Data
{
    public class PWSDbContext : DbContext
    {
        public PWSDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
