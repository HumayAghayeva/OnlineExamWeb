using Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Write
{
    public record  StudentPhotoDto
    {
        [Key]
        public int? Id { get; set; }
        public int StudentId { get; set; }
        public string? FileName { get; set; }
        public string? PhotoPath { get; set; }
    }
}
