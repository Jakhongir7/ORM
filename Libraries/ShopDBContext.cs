using Libraries.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Libraries
{
    public class ShopDBContext : DbContext
    {
        public ShopDBContext()
        {
            Database.EnsureCreated();
        }

        public ShopDBContext(DbContextOptions<ShopDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}
