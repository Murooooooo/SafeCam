﻿using Microsoft.AspNetCore.Identity;

namespace SafeCam.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
