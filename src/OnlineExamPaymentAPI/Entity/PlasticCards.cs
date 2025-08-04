using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineExamPaymentAPI.Entity
{
    public class PlasticCards
    {
        [Key]
        public int ID {get;set;}
        public string HolderName { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string CVV { get; set; }
    }
}
