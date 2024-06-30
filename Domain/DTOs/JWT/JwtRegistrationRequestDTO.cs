using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.JWT
{
    public class JwtRegistrationRequestDTO
    {
        [Required(ErrorMessage = "The Name field is required")]
        public string Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "The Email field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field  is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The ConfirmPassword field  is required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
