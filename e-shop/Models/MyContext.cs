using ConsoleApp1;
using Microsoft.EntityFrameworkCore;
using e_shop.Models;

namespace e_shop.Models
{
    public class MyContext : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }        
        public DbSet<Feedback> Feedback { get; set; }        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseMySQL("server=;database=;user=;password=;SslMode=none");            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne<CustomerDetail>(o => o.CustomerDetails)
                .WithOne(cd => cd.Order);
        }
    }
}
