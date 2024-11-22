using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Write
{
    public class StudentRequestDTO
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }   
        public string? DateOfBirth { get; set; }
        public string? PIN { get; set; }
        public Groups GroupId { get; set; }
        public int StudentId {  get; set; }
        public string? PhotoUrls { get; set; }   
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
