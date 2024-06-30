using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataContext.Write
{
    public class DBConn : IdentityDbContext
    {
        public DBConn(DbContextOptions<DBConn> options) : base(options)
        {

        }
    }
}
