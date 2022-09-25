﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeCrudTaskAPi.Resource.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
