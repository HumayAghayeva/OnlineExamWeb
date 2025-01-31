using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OnlineExamWeb.Middleware
{
    public class DatabaseHealthCheck<TContext> : IHealthCheck where TContext : DbContext
    {
        private readonly TContext _dbContext;

        public DatabaseHealthCheck(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                var Ocg = await _dbContext.Database.ExecuteSqlRawAsync("SELECT 1", cancellationToken); 
                return HealthCheckResult.Healthy("Database is up and running.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Database is unavailable.", ex);
            }
        }
    }
}