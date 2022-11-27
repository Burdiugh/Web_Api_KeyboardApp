using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
        public class DownloadFileDTO
        {
            public Stream Stream { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
        }
}
