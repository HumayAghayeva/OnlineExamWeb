using Infrastructure.DataContext.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.Read
{
    public class OEPReadDBContextFactory : IDesignTimeDbContextFactory<OEPReadDB>
    {
        public OEPReadDB CreateDbContext(string[] args)
        {

            var basePath = Directory.GetCurrentDirectory();

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var connectionString = configuration.GetConnectionString("ReadDbContext");

            var optionsBuilder = new DbContextOptionsBuilder<OEPReadDB>();

            optionsBuilder.UseSqlServer(connectionString);

            return new OEPReadDB(optionsBuilder.Options);
        }
    }
}