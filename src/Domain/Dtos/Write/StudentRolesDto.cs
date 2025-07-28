using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Write
{
    public class StudentRolesDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int RoleId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
