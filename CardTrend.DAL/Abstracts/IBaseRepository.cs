using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.Abstracts
{
    public interface IBaseRepository<T>
    {
        T Get(object key);
        T Get(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        //IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Delete(T entity);
        void Add(T entity);

    }
}
