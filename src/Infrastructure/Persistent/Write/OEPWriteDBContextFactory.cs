using Infrastructure.DataContext.Write;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistent.Write
{
    public class OEPWriteDBContextFactory : IDesignTimeDbContextFactory<OEPWriteDB>
    {
        public OEPWriteDB CreateDbContext(string[] args)
        {
          
            var basePath = Directory.GetCurrentDirectory();

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile($"appsettings.{environment}.json", optional: true) 
                .Build();

            var connectionString = configuration.GetConnectionString("WriteDbContext");

            var optionsBuilder = new DbContextOptionsBuilder<OEPWriteDB>();

            optionsBuilder.UseSqlServer(connectionString);

            return new OEPWriteDB(optionsBuilder.Options);
        }
    }
}
