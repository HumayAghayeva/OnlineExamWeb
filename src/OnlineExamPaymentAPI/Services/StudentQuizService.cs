using AutoMapper;
using Domain.Contract;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using OnlineExamPaymentAPI.DbConn;
using OnlineExamPaymentAPI.Dtos.Request;
using OnlineExamPaymentAPI.Entity;
using OnlineExamPaymentAPI.Interfaces;

namespace OnlineExamPaymentAPI.Services
{
    public class StudentQuizService : IStudentQuizService
    {
        private readonly OnlineExamDbContext _dbContext;

        private readonly DbSet<PlasticCardDto> _plasticCardDto;
        private readonly IMapper _mapper;

        public StudentQuizService(OnlineExamDbContext dbContext, DbSet<PlasticCardDto> plasticCardDto, IMapper mapper)
        {
            _dbContext = dbContext;
            _plasticCardDto = plasticCardDto;
            _mapper = mapper;
        }

        public Task<ApiResponse> CreateStudentQuizAsync(StudentQuizzesDto studentQuizzes, CancellationToken cancellationToken)
        {

            var studentQuizzesDto = new StudentQuizzesDto
            {
                StudentID = studentQuizzes.StudentID,
                PlasticCardID = studentQuizzes.PlasticCardID,
                QuizDate = studentQuizzes.QuizDate,
                QuizPrice = studentQuizzes.QuizPrice,

            };

            var _studentQuizzesDto = _mapper.Map<StudentQuizzes>(studentQuizzesDto);

        }
    }
}
