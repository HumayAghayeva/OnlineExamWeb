using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract
{
    public class NotFoundException :Exception
    {
        public readonly ResponseCode Code;

        public NotFoundException() : base(ResponseMessage.ResponseMessages[ResponseCode.DataNotFound])
        {
            Code = ResponseCode.DataNotFound;
        }

        public NotFoundException( string message) : base(ResponseMessage.ResponseMessages[ResponseCode.DataNotFound])
        {
            Code=ResponseCode.DataNotFound;
        }
    }
}
