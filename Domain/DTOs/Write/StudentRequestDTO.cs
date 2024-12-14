using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Write
{
    public record StudentRequestDTO
    {
        [Required(ErrorMessage = "The Name field is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The LastName field is required")]
        public string? LastName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "The Email field is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "The Email field is required")]
        public string? DateOfBirth { get; set; }
        [Required(ErrorMessage = "The PIN field is required")]
        public string? PIN { get; set; }
        public Enums.Groups GroupId { get; set; }
        [Required(ErrorMessage = "The Password field  is required")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "The ConfirmPassword field  is required")]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }
}
