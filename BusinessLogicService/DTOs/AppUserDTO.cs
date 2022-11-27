using Core.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class AppUserDTO 
    {
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string>? Roles { get; set; }
        public ICollection<AppScore> Scores { get; set; }
        public IFormFile Image { get; set; }
    }
}


