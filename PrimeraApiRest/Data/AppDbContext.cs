using Microsoft.EntityFrameworkCore;
using PrimeraApiRest.Models;

namespace PrimeraApiRest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AtlasPhoto> Photos { get; set; }
    }
}
