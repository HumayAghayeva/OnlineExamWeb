using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Read
{
    public record FileResponse
    {
       public string FileName { get; set; }
       public string FilePath { get; set; }
    }
}
