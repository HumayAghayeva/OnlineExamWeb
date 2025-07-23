using Domain.DTOs.Read;

namespace OnlineExamWebApi.Interfaces
{
    public interface IStudentRepository
    {
        Task<ResponseDto> ConfirmStudent(int studentId, CancellationToken cancellationToken);
    }
}
