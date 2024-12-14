using Abstraction.Command;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineExamWeb.Controllers
{
    public class ConfirmController : Controller
    {
        private readonly IStudentCommandRepository _commandRepository;

        public ConfirmController(IStudentCommandRepository commandRepository)
        {
            _commandRepository = commandRepository; 
        }

        // GET: ConfirmController
        public async Task<ActionResult> Index(int studentId, CancellationToken cancellationToken)
        {
           var result= await _commandRepository.ConfirmStudent(studentId, cancellationToken);

           if(result.Success)
                return RedirectToAction("GetStudent", "StudentController", new { id = result.Id });
            else
                return RedirectToAction("Index");
        }

    }
}
