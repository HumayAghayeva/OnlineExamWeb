using Abstraction.Write;
using Domain.DTOs.Write;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineExamWeb.Controllers
{
    public class StudentController : BaseController
    {
        private IStudentPostRepository postRepository;
        // GET: StudentController
        public StudentController(IStudentPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentController/Create
        [HttpPost("student-management/register-new")]
        public async Task<IActionResult> Create(StudentRequestDTO studentRequestDTO,CancellationToken token)
        {
            if (!ModelState.IsValid) return View(studentRequestDTO);

            if (ResponseHasErrors(await postRepository.PostStudentAsync(studentRequestDTO,token)))
                return View(studentRequestDTO);

            ViewBag.Success = "Student Registered!";

            return View(studentRequestDTO); 
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
