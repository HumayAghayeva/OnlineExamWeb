﻿using Abstraction.Command;
using Microsoft.AspNetCore.Mvc;

namespace OnlineExamWebApi.Controllers
{
    [Route("api/student")]
    [ApiController] 
    public class ConfirmController : ControllerBase
    {
        private readonly IStudentCommandRepository _commandRepository;

        public ConfirmController(IStudentCommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        [HttpPut("confirm/{studentId}")]
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
