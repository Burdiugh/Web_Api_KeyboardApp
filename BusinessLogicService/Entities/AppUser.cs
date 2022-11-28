using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AppUser : IdentityUser
    {
        public ICollection<IdentityRole> Roles { get; set; }

        public ICollection<AppScore> Scores { get; set; } = new HashSet<AppScore>();

        public string? ImagePath { get; set; }

    }
}
