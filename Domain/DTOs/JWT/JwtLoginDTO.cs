using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.JWT
{
    public class JwtLoginDTO
    {
        [Required(ErrorMessage = "The Username field is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The Password field is required")]
        public string? Password { get; set; }
    }
}
