using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamPaymentAPI.Enums
{
    public  enum CardTypes
    {
        [Description("Visa")]
        Visa = 1,

        [Description("MasterCard")]
        MasterCard = 2
    }
}
