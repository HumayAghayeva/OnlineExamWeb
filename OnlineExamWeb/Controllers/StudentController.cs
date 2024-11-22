using Business.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Abstraction.Queries;
using Abstraction.Command;
using Domain.DTOs.Write;
using Domain.DTOs.Read;
using System.Threading;

namespace OnlineExamWeb.Controllers
{
    public class StudentController : Controller
    {
         private readonly IStudentCommandRepository _commandRepository;
         private readonly IStudentQueryRepository _studentQueryRepository;

        public StudentController(IStudentCommandRepository commandRepository, IStudentQueryRepository studentQueryRepository)
        {
          _commandRepository = commandRepository;
          _studentQueryRepository = studentQueryRepository;
        }
       
        public ActionResult Index(StudentResponseDTO student)
        {       
            return View(student);
        }

        public async Task<ActionResult> GetStudent(int studentId, CancellationToken cancellationToken)
        {
            var student = await _studentQueryRepository.GetStudentById(studentId, cancellationToken);
            return View(student);
        }

        public async Task<ActionResult> GetStudents(CancellationToken cancellationToken)
        {
            var students = await _studentQueryRepository.GetStudents(cancellationToken);
            return View(students);
        }

    
        public ActionResult CreateStudent()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStudent(StudentRequestDTO studentRequestDTO)
        {
            if (!ModelState.IsValid)            {
              
                return View(studentRequestDTO);
            }
            try
            {                
                await _commandRepository.AddStudent(studentRequestDTO, CancellationToken.None);

                return RedirectToAction(nameof(GetStudents));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the student.";                
                return View(studentRequestDTO);
            }
        }

        [HttpGet]
        public async Task<IActionResult> LoginStudent()
        {
            return  View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginStudent(StudentLoginDTO studentLoginDTO,CancellationToken cancellationToken)
        {
            var studentResponse= await _commandRepository.LoginStudent(studentLoginDTO, cancellationToken);

            if(studentResponse !=null)
                return RedirectToAction(nameof(GetStudent), new { studentId = studentResponse.Id });
            else
                throw new Exception("Invalid email or password.");
        }

      
        public ActionResult Edit(int id)
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    
        public ActionResult Delete(int id)
        {
            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
