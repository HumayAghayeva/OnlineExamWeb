﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class StudentPhoto
    {
        [Key]
        public int Id { get; set; } 
        public virtual Student StudentId { get; set; }    
        public string? Url {  get; set; }
    }
}