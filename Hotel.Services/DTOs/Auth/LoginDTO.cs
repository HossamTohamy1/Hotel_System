﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Auth
{
    public class LoginDTO
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
