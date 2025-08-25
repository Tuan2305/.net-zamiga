using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser : IdentityUser
    {
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}