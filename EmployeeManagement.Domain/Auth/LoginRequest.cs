﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.Auth
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }


}
