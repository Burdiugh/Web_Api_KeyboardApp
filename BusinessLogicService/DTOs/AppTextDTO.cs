using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class AppTextDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int LevelId { get; set; }
        public string ?LevelName { get; set; }
        public int LanguageId { get; set; }
        public string ?LanguageName { get; set; }
    }
}
