using Libraries;
using Libraries.Entities;
using Libraries.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Tests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        [Test]
        public void Create_Product_InsertsProduct()
        {
            using var context = new ShopDBContext(
                new DbContextOptionsBuilder<ShopDBContext>()
                    .UseInMemoryDatabase("ShopDBContext1")
                    .Options);
            context.Database.EnsureDeleted();

            var expected = new Product
            {
                Id = 1,
                Name = "ProductName1",
                Description = "ProductDescription",
                Height = 100,
                Length = 200,
                Weight = 101,
                Width = 200
            };

            var productRepository = new ProductRepository(context);

            productRepository.Create(new Product
            {
                Name = "ProductName1",
                Description = "ProductDescription",
                Height = 100,
                Length = 200,
                Weight = 101,
                Width = 200
            });

            var product = context.Product.Single();

            Assert.AreEqual(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(product));
        }

        [Test]
        public void Delete_Product_DeletesProduct()
        {
            using var context = new ShopDBContext(
                new DbContextOptionsBuilder<ShopDBContext>()
                    .UseInMemoryDatabase("ShopDBContext2")
                    .Options);
            context.Database.EnsureDeleted();

            var expected = new Product
            {
                Id = 1,
                Name = "ProductName1",
                Description = "ProductDescription",
                Height = 100,
                Length = 200,
                Weight = 101,
                Width = 200
            };

            context.Product.Add(expected);
            context.SaveChanges();

            var productRepository = new ProductRepository(context);

            productRepository.Delete(expected);

            var actualCount = context.Product.Count();

            var expectedCount = 0;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void Update_Product_UpdatesProduct()
        {
            using var context = new ShopDBContext(
                new DbContextOptionsBuilder<ShopDBContext>()
                    .UseInMemoryDatabase("ShopDBContext3")
                    .Options);
            context.Database.EnsureDeleted();

            var product = new Product
            {
                Id = 1,
                Name = "ProductName1",
                Description = "ProductDescription",
                Height = 100,
                Length = 200,
                Weight = 101,
                Width = 200
            };

            context.Product.Add(product);
            context.SaveChanges();


            var productRepository = new ProductRepository(context);
            var expected = context.Product.Single();
            expected.Name = "new name";
            expected.Description = "new desc";

            productRepository.Update(expected);

            var actualProduct = context.Product.Single();

            Assert.AreEqual(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actualProduct));
        }

        [Test]
        public void Read_Product_ReturnsProduct()
        {
            using var context = new ShopDBContext(
                new DbContextOptionsBuilder<ShopDBContext>()
                    .UseInMemoryDatabase("ShopDBContext4")
                    .Options);
            context.Database.EnsureDeleted();

            var expected = new Product
            {
                Id = 1,
                Name = "ProductName1",
                Description = "ProductDescription",
                Height = 100,
                Length = 200,
                Weight = 101,
                Width = 200
            };

            context.Product.Add(expected);
            context.SaveChanges();

            var productRepository = new ProductRepository(context);

            var actualOrder = productRepository.Read(1);

            Assert.AreEqual(JsonSerializer.Serialize(expected), JsonSerializer.Serialize(actualOrder));
        }
    }
}
