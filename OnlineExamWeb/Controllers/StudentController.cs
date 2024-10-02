using Business.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Abstraction.Queries;
using Abstraction.Command;
using Domain.DTOs.Write;

namespace OnlineExamWeb.Controllers
{
    public class StudentController : Controller
    {
         private readonly IStudentCommandRepository _commandRepository;

        public StudentController(IStudentCommandRepository commandRepository)
        {
          _commandRepository = commandRepository;
        }
        // GET: StudentController
        public ActionResult Index(CancellationToken cancellationToken)
        {       
            return View();
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentRequestDTO studentRequestDTO)
        {
            if (!ModelState.IsValid)            {
              
                return View(studentRequestDTO);
            }
            try
            {                
                await _commandRepository.AddStudent(studentRequestDTO, CancellationToken.None);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the student.";                
                return View(studentRequestDTO);
            }
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
