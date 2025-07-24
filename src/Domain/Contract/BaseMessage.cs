using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract
{
    public abstract class BaseMessage
    {
        public string CorrelationId { get; set; }

        public string ApplicationName { get; set; }
    }
}
