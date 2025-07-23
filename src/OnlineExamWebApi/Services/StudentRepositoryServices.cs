using Domain.DTOs.Read;
using Microsoft.EntityFrameworkCore;
using OnlineExamWebApi.DBContext;
using OnlineExamWebApi.Interfaces;

namespace OnlineExamWebApi.Services
{
    public class StudentRepositoryServices : IStudentRepository
    {
        private readonly OEPWriteDB _context;

        public StudentRepositoryServices(OEPWriteDB context)
        {
            _context = context;
        }

        public async Task<ResponseDto> ConfirmStudent(int studentId, CancellationToken cancellationToken)
        {

            
                var student = await _context.Students
               .Where(w => w.ID == studentId).FirstOrDefaultAsync(cancellationToken);

                if (student == null)
                {
                    throw new Exception("Invalid data.");
                }

                student.IsConfirmed = true;

                _context.Students.Update(student);

                await _context.SaveChangesAsync(cancellationToken);

                return new ResponseDto
                {
                    Success = true,
                    Message = "Student was confirmed successfully.",
                    Id = student.ID
                };
          
           
        }
    }
}
