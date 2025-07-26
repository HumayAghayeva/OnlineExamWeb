using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamPaymentAPI.Helper
{
    public static class PlasticCardValidatorHelper
    {
        public static async Task<bool> IsValidLuhn(string number)
        {
            int sum = 0;
            bool alternate = false;

            for (int i = number.Length - 1; i >= 0; i--)
            {
                int n = number[i] - '0';

                if (alternate)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }

                sum += n;
                alternate = !alternate;
            }

            return (sum % 10 == 0);
        }

        public static async Task<bool>  IsSupportedCardType(string number)
        {
            // Visa: starts with 4
            if (number.StartsWith("4"))
                return true;

            // MasterCard: 51–55 or 2221–2720
            if (number.Length >= 4)
            {
                int prefix2 = int.Parse(number.Substring(0, 2));
                int prefix4 = int.Parse(number.Substring(0, 4));

                if ((prefix2 >= 51 && prefix2 <= 55) || (prefix4 >= 2221 && prefix4 <= 2720))
                    return true;
            }

            return false;
        }
    }
}
