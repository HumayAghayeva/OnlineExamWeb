using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class UserSessions
    {
        public Guid SessionId { get; set; }
        public int StudentId { get; set; }
        public DateTime LoginDate { get; set; }
    }
}
