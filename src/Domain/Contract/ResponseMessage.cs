using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract
{
    public class ResponseMessage
    {

        public static readonly Dictionary<ResponseCode, string> ResponseMessages
            = new Dictionary<ResponseCode, string>
        {
            { ResponseCode.Success, "Success" },
            { ResponseCode.DataNotFound, "Data not found" },
            { ResponseCode.InternalServerError, "Internal server error occured" },
            { ResponseCode.ServiceProviderError, "Error occured in service provider side" },
            { ResponseCode.AuthroizationError, "No privilege" },
            { ResponseCode.AuthenticationError, "Not authenticated" },
            { ResponseCode.ValidationError, "Invalid request" },
};
    }
}
