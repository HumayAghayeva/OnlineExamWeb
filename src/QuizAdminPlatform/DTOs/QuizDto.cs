namespace QuizAdminPlatform.DTOs
{
    public record QuizDto
    {
        public int Id { get;set; }
        public string Duration { get; set; }   
        public int QuestionCount { get; set; }   
    }
}
