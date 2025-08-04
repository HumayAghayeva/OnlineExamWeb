namespace OnlineExamPaymentAPI.Entity
{
    public class StudentQuizzes
    {
        public int Id { get; set; }
        public int StudentID { get; set; }
        public int PlasticCardID { get; set; }
        public double QuizPrice { get; set; }
        public DateTime QuizDate { get; set; }
    }
}