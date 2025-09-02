using System.ComponentModel.DataAnnotations;

namespace OnlineExamPaymentAPI.Dtos.Request
{
    public record UserPlasticCardDto
    {
        public int UserID { get; set; }
        public int PlasticCardID { get; set; }
        public Guid TransactionID { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
