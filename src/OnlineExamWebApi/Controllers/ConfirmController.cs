using Microsoft.AspNetCore.Mvc;
using OnlineExamWebApi.Interfaces;

namespace OnlineExamWebApi.Controllers
{
    [Route("api/student")]
    [ApiController] 
    public class ConfirmController : ControllerBase
    {
        private readonly IStudentRepository _commandRepository;
        public ConfirmController(IStudentRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        [HttpGet("confirm/{studentId}")]
        public async Task<IActionResult> ConfirmStudent(int studentId, CancellationToken cancellationToken)
        {
            var result = await _commandRepository.ConfirmStudent(studentId, cancellationToken);
          
            if (!result.Success)
            {
                return NotFound("Student not found or confirmation failed.");
            }

            return NoContent();
        }
    }
}
