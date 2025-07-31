using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Student : BaseEntity
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? PIN { get; set; }
        public int GroupId { get; set; }
        public string? Email { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        
    }
}
