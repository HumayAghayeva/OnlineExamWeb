using Infrastructure.DataContext.Write;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.Write
{
    public class OEPWriteDBContextFactory : IDesignTimeDbContextFactory<OEPWriteDB>
    {
        public OEPWriteDB CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OEPWriteDB>();
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=OEPWriteDB;Integrated Security=True;TrustServerCertificate=True;");

            return new OEPWriteDB(optionsBuilder.Options);
        }
    }
}
