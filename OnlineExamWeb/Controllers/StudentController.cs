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
        // GET: StudentController
        public ActionResult Index(StudentResponseDTO student)
        {       
            return View(student);
        }
        public async Task<ActionResult> GetStudent(int studentId, CancellationToken cancellationToken)
        {
            var student = await _studentQueryRepository.GetStudentById(1005,cancellationToken);
            return View(student);
        }
        // GET: StudentController
        public async Task<ActionResult> GetStudents(CancellationToken cancellationToken)
        {
            var students = await _studentQueryRepository.GetStudents(cancellationToken);
            return View(students);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult CreateStudent()
        {
            return View();
        }

        // POST: StudentController/Create
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
        
        public ActionResult Login(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(StudentLoginDTO studentLoginDTO,CancellationToken cancellationToken)
        {
            var studentResponse= await _commandRepository.LoginStudent(studentLoginDTO, cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentController/Edit/5
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

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentController/Delete/5
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
