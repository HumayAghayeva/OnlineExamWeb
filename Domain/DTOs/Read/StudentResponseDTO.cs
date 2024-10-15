using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Read
{
    public class StudentResponseDTO
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? PIN { get; set; }
        public string? GroupName { get; set; }
    }
}
