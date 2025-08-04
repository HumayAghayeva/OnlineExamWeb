namespace OnlineExamPaymentAPI.Dtos.Request
{
    public record StudentQuizzesDto
    {
        public int Id { get; init; }
        public int StudentID { get; init; }
        public int PlasticCardID { get; init; }
        public double QuizPrice { get; init; }
        public DateTime QuizDate { get; init; }
    }
}
