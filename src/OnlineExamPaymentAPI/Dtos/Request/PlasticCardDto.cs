using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamPaymentAPI.Dtos.Request
{
    using System.ComponentModel.DataAnnotations;

    public record PlasticCardDto
    {
        public int ID { get; init; }
        [Required(ErrorMessage = "Card holder name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Holder name must be between 2 and 50 characters.")]
        public string HolderName { get; init; }

        [Required(ErrorMessage = "Card number is required.")]
        [RegularExpression(@"^\d{16,17}$", ErrorMessage = "Card number must be between 16 and 17 digits.")]
        public string CardNumber { get; init; }

        
        [RegularExpression(@"^(0?[1-9]|1[0-2])$", ErrorMessage = "Expiration month must be a valid 1–12 value.")]
        public string ExpireMonth { get; init; }

        [Required(ErrorMessage = "Expiration year is required.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Expiration year must be a 4-digit number.")]
        public string ExpireYear { get; init; }

        [Required(ErrorMessage = "CVV is required.")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV must be 3 or 4 digits.")]
        public string CVV { get; init; }

        public string CardType { get; init; } = null;
    }

}
