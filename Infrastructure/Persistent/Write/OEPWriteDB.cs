using Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataContext.Write
{
    public class OEPWriteDB : DbContext
    {
        public OEPWriteDB(DbContextOptions<OEPWriteDB> options) : base(options)
        {

        }
        public virtual DbSet<Student> Students { get; set; }
    }
    //public class YourDbContextFactory : IDesignTimeDbContextFactory<DBConn>
    //{
    //    public DBConn CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<DBConn>();
    //        optionsBuilder.UseSqlServer(DBConn);

    //        return new DBConn(optionsBuilder.Options);
    //    }
    //}
}
