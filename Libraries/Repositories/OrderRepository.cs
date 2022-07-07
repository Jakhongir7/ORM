using Libraries;
using Libraries.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Libraries.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(ShopDBContext context) : base(context)
        {
        }

        public IEnumerable<Order> GetOrders(int? month = null, OrderStatus? status = null, int? year = null, int? productId = null)
        {
            var monthParam = month == null ? new SqlParameter("@Month", DBNull.Value) : new SqlParameter("@Month", month);
            var statusParam = status == null ? new SqlParameter("@Status", DBNull.Value) : new SqlParameter("@Status", status);
            var yearParam = year == null ? new SqlParameter("@Year", DBNull.Value) : new SqlParameter("@Year", year);
            var productIdParam = productId == null ? new SqlParameter("@ProductId", DBNull.Value) : new SqlParameter("@ProductId", productId);

            return Context.Order.FromSqlRaw("sp_FetchOrders @Month, @Year, @Status, @ProductId",
                    monthParam, yearParam, statusParam, productIdParam)
                .ToList();
        }

        public void DeleteOrders(int? month = null, OrderStatus? status = null, int? year = null,
            int? productId = null)
        {
            var monthParam = month == null ? new SqlParameter("@Month", DBNull.Value) : new SqlParameter("@Month", month);
            var statusParam = status == null ? new SqlParameter("@Status", DBNull.Value) : new SqlParameter("@Status", status);
            var yearParam = year == null ? new SqlParameter("@Year", DBNull.Value) : new SqlParameter("@Year", year);
            var productIdParam = productId == null ? new SqlParameter("@ProductId", DBNull.Value) : new SqlParameter("@ProductId", productId);

            var ordersToDelete = Context.Order.FromSqlRaw("sp_DeleteOrders @Month, @Year, @Status, @ProductId",
                    monthParam, yearParam, statusParam, productIdParam)
                .ToList();

            Context.Order.RemoveRange(ordersToDelete);
            Context.SaveChanges();
        }
    }
}
