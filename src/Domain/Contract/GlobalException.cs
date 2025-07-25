using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract
{
    public  class GlobalException:Exception
    {
        public readonly ResponseCode Code;

        public GlobalException(ResponseCode code) : base(ResponseMessage.ResponseMessages[code])
        {
            Code= code;
        }
        public GlobalException(ResponseCode code, string message) : base(message) {

            Code = code;
        }
    }
}
