using Domain.Contract;
using OnlineExamPaymentAPI.Dtos.Request;

namespace OnlineExamPaymentAPI.Interfaces
{
    public interface IStudentQuizService
    {
        Task<ApiResponse> CreateStudentQuizAsync(StudentQuizzesDto studentQuizzesDto , CancellationToken cancellationToken);
    }
}
