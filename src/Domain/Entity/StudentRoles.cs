using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class StudentRoles
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int RoleId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
