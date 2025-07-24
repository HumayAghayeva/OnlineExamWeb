using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.Enums;

namespace Domain.Contract
{
    public class NoPriviledgeException : Exception
    {
        public readonly ResponseCode Code;

        public NoPriviledgeException() : base(ResponseMessage.ResponseMessages[ResponseCode.AuthenticationError])
        {
            Code = ResponseCode.AuthroizationError;
        }

        public NoPriviledgeException(string message) : base(message)
        {
            Code = ResponseCode.AuthroizationError;
        }
    }
}
