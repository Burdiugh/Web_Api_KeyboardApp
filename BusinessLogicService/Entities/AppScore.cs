using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AppScore
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public float Score { get; set; }
        public int Accuracy { get; set; }
        public int Errors { get; set; }
        public float Speed { get; set; }

    }
}
