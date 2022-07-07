using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Repositories
{
    public interface IRepository<T>
    {
        void Create(T entity);

        T Read(int id);

        void Update(T entity);

        void Delete(T entity);
    }
}
