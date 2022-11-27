using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ScoreDTO
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public float Score { get; set; }
        public int Accuracy { get; set; }
        public int Errors { get; set; }
        public float Speed { get; set; }
    }
}
