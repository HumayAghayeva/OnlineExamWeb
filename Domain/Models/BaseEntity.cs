using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "The Email field is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password field  is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The ConfirmPassword field  is required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime UpdatedTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
