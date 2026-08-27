using Microsoft.EntityFrameworkCore;
using SalesWebApi.Data.Entities;

namespace SalesWebApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Sale> Sale { get; set; }
        public DbSet<SaleDetail> SaleDetail { get; set; }
    }
}
