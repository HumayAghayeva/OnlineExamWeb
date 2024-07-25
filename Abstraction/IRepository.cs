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
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        T GetById(int id);
        T GetByIdWithIncludes(int id);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdWithIncludesAsync(int id);
        bool Remove(int id);
        void Add(in T sender);
        void Update(in T sender);
        int Save();
        Task<int> SaveAsync();
        public T Select(Expression<Func<T, bool>> predicate);
        public Task<T> SelectAsync(Expression<Func<T, bool>> predicate);
    }
    
}
