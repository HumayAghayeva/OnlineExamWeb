using Domain.Dtos.Write;
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
        public virtual DbSet<StudentPhoto> StudentPhotos { get; set; }
        public virtual DbSet<StudentRoles> StudentRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<StudentRoles>();
            modelBuilder.Entity<StudentRoles>().ToTable("StudentRoles");
        }


    }
 
}
