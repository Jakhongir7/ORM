using Libraries;
using Libraries.Entities;
using Libraries.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShopDBContext>()
                .UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Shop;Integrated Security=True;TrustServerCertificate=True")
                .Options;

            var context = new ShopDBContext(optionsBuilder);

            var productRepo = new ProductRepository(context);

            var products = productRepo.GetAll();

            foreach (var product in products)
            {
                Console.WriteLine(product.Name);
            }

            var orderRepo = new OrderRepository(context);
            //orderRepo.Create(new Order()
            //{
            //    CreatedDate = DateTime.Now,
            //    ProductId = 1,
            //    Status = OrderStatus.Loading,
            //    UpdatedDate = DateTime.Now + TimeSpan.FromHours(5)
            //});

            var orders = orderRepo.GetOrders(status: OrderStatus.Loading);

            foreach (var order in orders)
            {
                Console.WriteLine(order.Id);
            }
        }
    }
}
