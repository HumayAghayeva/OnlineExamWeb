using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public  enum ResponseCode
    {
        Success = 2000,
        DataNotFound= 2001,
        InternalServerError=2002,
        ValidationError =2003,
        AuthroizationError=2004,
        AuthenticationError=2005,
        ServiceProviderError=2006,
        InvalidForeignKeyException=2007
    }
}
