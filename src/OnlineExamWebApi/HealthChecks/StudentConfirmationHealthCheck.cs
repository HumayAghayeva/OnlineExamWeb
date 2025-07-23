using Microsoft.Extensions.Diagnostics.HealthChecks;
using OnlineExamWebApi.Services;

namespace OnlineExamWebApi.HealthChecks
{
    public class StudentConfirmationHealthCheck : IHealthCheck
    {
        private readonly StudentRepositoryServices _studentCommandRepository;

        public StudentConfirmationHealthCheck(StudentRepositoryServices studentCommandRepository)
        {
            _studentCommandRepository = studentCommandRepository
;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // Replace with a valid studentId for testing health check
            int testStudentId = 1;
            var result = await _studentCommandRepository.ConfirmStudent(testStudentId, cancellationToken);

            if (result.Success)
            {
                return HealthCheckResult.Healthy("Student confirmation API is working.");
            }

            return HealthCheckResult.Unhealthy("Student confirmation API failed.");
        }
    }
}