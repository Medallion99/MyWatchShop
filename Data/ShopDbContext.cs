using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWatchShop.Models.Entity;

namespace MyWatchShop.Data
{
    public class ShopDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Product> Products { get; set; }    
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

    }
}
