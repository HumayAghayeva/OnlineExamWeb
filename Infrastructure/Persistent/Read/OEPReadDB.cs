using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistent.Read
{
    public class OEPReadDB : DbContext
    {
        public OEPReadDB(DbContextOptions<OEPReadDB> options) : base(options)
        {

        }
        public virtual DbSet<Student> Students { get; set; }
    }
}
