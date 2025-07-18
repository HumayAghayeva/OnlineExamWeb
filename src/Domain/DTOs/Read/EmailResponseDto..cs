using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Read
{
    public class EmailResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public bool IsConfirmed { get; set; }
        public Guid TransactionId { get; set; }
        public string ErrorDetails { get; set; }

    }
}
