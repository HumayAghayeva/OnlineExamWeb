using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        // Start the database Transaction
        Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);

        // Commit the database Transaction
        Task CommitAsync(CancellationToken cancellationToken = default);

        // Rollback the database Transaction
        Task RollbackAsync(CancellationToken cancellationToken = default);

        // DbContext Class SaveChanges method
        Task SaveAsync(CancellationToken cancellationToken = default);

        IRepository<T> Repository<T>() where T : class;
    }
}
