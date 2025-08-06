namespace OnlineExamPaymentAPI.Dtos.Request
{
    public record UserLoginDto
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
