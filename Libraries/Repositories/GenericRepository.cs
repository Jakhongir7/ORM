using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected ShopDBContext Context { get; }

        protected GenericRepository(ShopDBContext context)
        {
            Context = context;
        }

        public void Create(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        public T Read(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        public void Delete(T entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }
    }
}
