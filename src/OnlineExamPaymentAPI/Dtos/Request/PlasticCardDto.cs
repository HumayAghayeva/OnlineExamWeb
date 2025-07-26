using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamPaymentAPI.Dtos.Request
{
    using System.ComponentModel.DataAnnotations;

    public class PlasticCardDto
    {
        [Required(ErrorMessage = "Card holder name is required.")]
        [StringLength(100, ErrorMessage = "Holder name can't exceed 100 characters.")]
        public string HolderName { get; set; }

        [Required(ErrorMessage = "Card number is required.")]
        [RegularExpression(@"^\d{13,19}$", ErrorMessage = "Card number must be between 13 and 19 digits.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Expiration month is required.")]
        [RegularExpression(@"^(0?[1-9]|1[0-2])$", ErrorMessage = "Expiration month must be a valid 1–12 value.")]
        public string ExpireMonth { get; set; }

        [Required(ErrorMessage = "Expiration year is required.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Expiration year must be a 4-digit number.")]
        public string ExpireYear { get; set; }

        [Required(ErrorMessage = "CVV is required.")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV must be 3 or 4 digits.")]
        public string CVV { get; set; }
    }

}
