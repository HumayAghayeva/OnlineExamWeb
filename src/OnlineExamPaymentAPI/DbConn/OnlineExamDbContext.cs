using Microsoft.EntityFrameworkCore;
using OnlineExamPaymentAPI.Dtos.Request;
using OnlineExamPaymentAPI.Entity;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace OnlineExamPaymentAPI.DbConn
{

    public class OnlineExamDbContext : DbContext
    {
        public OnlineExamDbContext(DbContextOptions<OnlineExamDbContext> options)
            : base(options)
        {

        }

        public DbSet<PlasticCards> PlasticCards { get; set; }
        public DbSet<StudentQuizzes> StudentQuizzes { get; set; }
         public DbSet<UserPlasticCard> UserPlasticCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PlasticCards>().ToTable("PlasticCards");
            modelBuilder.Entity<StudentQuizzes>().ToTable("StudentQuizzes");
            modelBuilder.Entity<UserPlasticCard>().ToTable("UserPlasticCards");
         
        }
    }
}