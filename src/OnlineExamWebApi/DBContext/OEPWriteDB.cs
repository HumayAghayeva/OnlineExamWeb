using Domain.Entity.Write;
using Microsoft.EntityFrameworkCore;

namespace OnlineExamWebApi.DBContext
{
    public class OEPWriteDB : DbContext
    {
        public OEPWriteDB(DbContextOptions<OEPWriteDB> options) : base(options)
        {

        }
        public virtual DbSet<Student> Students { get; set; }
    }
}
