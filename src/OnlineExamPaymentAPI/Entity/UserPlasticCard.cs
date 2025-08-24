using Domain.DTOs.Write;
using System.ComponentModel.DataAnnotations;

namespace OnlineExamPaymentAPI.Entity
{
    public class UserPlasticCard
    {
        [Key]
        public int ID { get; set; }
        public int UserId { get; set; }
        public int PlasticCardID { get; set; }

    }
}
