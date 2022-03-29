using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Data
{
    public class CiberDbContext : DbContext
    {
        public CiberDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=Cibers;User ID=sa;Password=123456");
        }

        public CiberDbContext(DbContextOptions<CiberDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           builder.Entity<Order>().Property(e => e.OrderDate).HasColumnType("datetime");
        }
    }
}