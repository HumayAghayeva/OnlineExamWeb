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
        private IDbContextTransaction _transaction;

        public UnitOfWork(DbContext context)
        {
            _context = context;
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

        public IRepository<T> Repository<T>() where T : class
        {
           
            return new Repository<T>(_context);
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