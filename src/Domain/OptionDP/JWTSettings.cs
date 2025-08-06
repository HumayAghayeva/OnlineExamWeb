﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.OptionDP
{
    public class JWTSettings
    {
        public string SecretKey { get; set; } 
        public string Issuer { get; set; } 
        public string Audience { get; set; } 
        public int TokenValidityInMinutes { get; set; }
    }
}
