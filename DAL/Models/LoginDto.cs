﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DAL.Models
{
    public class LoginDto
    {
            [EmailAddress]
            public string Email { get; set; }
            public string Password { get; set; }
       
    }

}

