using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AppText
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
