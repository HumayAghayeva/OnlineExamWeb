using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Read
{
    public class QuizMessage
    {
        public string Question { get; set; }
        public string[] Options { get; set; }
    }
}
