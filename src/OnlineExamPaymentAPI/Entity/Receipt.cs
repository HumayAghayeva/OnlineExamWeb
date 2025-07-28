namespace OnlineExamPaymentAPI.Entity
{
    public class Receipt
    {
        public int Id { get; set; } 
        public int StudentId { get; set; }
        public string TransactionId { get; set; }
        public DateTime PayDate { get; set; }
        public string Description { get; set; }
    }
}
