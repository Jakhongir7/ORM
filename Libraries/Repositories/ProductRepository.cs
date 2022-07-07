using Libraries.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Libraries.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(ShopDBContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetAll()
        {
            return Context.Product.ToList();
        }
    }
}
