namespace OnlineExamPaymentAPI.Dtos.Request
{
    public class StudentQuiz
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int PlasticCardId { get; set; }
        public double QuizPrice { get; set; }
        public DateTime QuizDate { get; set; }
    }
}
