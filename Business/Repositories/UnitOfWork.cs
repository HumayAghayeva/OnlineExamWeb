using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using Infrastructure.DataContext.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private  IDbContextTransaction _transaction;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(DbContext context, IDbContextTransaction transaction, Dictionary<Type, object> repositories)
        {
            _context = context;
            _transaction = transaction;
            _repositories = repositories;   
        }

        public async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
            CancellationToken cancellationToken = default)
        {
            _transaction = await _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _transaction.CommitAsync(cancellationToken);
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _transaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }
        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.TryGetValue(typeof(T), out var repository))
            {
                return (IRepository<T>)repository;
            }

            var newRepository = new Repository<T>(_context); // Assumes a Repository<T> class exists
            _repositories.Add(typeof(T), newRepository);

            return newRepository;
        }


        #region Dispose

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}