using Domain.Models.Write;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamWeb.DataContext.Write
{
    public class DbConnWrite : IdentityDbContext
    {
        public DbConnWrite(DbContextOptions<DbConnWrite> options) : base(options)
        {

        }
        public DbSet<Student>Students { get; set; } 
    }
}
