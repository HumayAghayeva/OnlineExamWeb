using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Read
{
    public class Student : BaseEntity
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? PIN { get; set; }
        public string? GroupName { get; set; }
        public string? Email { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
