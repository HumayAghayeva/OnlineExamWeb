using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Student :BaseEntity
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? PIN { get; set; }
        public int GroupId { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "The Email field is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The Password field  is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "The ConfirmPassword field  is required")]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
