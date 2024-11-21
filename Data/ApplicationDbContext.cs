using Microsoft.EntityFrameworkCore;
using Tiki_Web_Project.Models;

namespace Tiki_Web_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        { 
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategorie> subCategories { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<QuickLink> QuickLinks { get; set; }

        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        
        public DbSet<Image> Images { get; set; }

    }
}
