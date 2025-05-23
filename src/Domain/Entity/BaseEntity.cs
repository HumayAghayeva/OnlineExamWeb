﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }        
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime UpdatedTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
