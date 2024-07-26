using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetByIdAsync(object id);
        Task<IEnumerable<T>> GetAllAsync();
    }
    
}
