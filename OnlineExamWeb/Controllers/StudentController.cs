using Business.Repositories;
using Microsoft.AspNetCore.Mvc;
using Abstraction.Queries;
using Abstraction.Command;
using Domain.DTOs.Write;
using Domain.DTOs.Read;
using System.Threading;
using Domain.Entity;
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
        public async Task<IActionResult> CreateStudent(StudentRequestDTO studentRequestDTO,IFormCollection formCollection,  CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) {
              
                return View(studentRequestDTO);
            }
            try
            {
                var uploadedPhoto = formCollection.Files["studentPhoto"];

                var student = await _commandRepository.AddStudent(studentRequestDTO, cancellationToken);

                if (student.Success == false)
                    throw new Exception("student elave olunmadi");
                else
                if (uploadedPhoto != null && uploadedPhoto.Length > 0)
                {
                    string photoPath;

                    string photosDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "StudentPhotos");

                    if (!Directory.Exists(photosDirectory))
                    {
                        Directory.CreateDirectory(photosDirectory);
                    }
                    string fileName = uploadedPhoto.FileName;

                    photoPath = Path.Combine(photosDirectory, fileName);


                    using (var stream = new FileStream(photoPath, FileMode.Create))
                    {
                        await uploadedPhoto.CopyToAsync(stream);
                    }

                    var studentPhotoDto = new StudentPhotoDTO
                    {
                        PhotoPath = photoPath,
                        StudentId = student.Id,
                        FileName = fileName
                    };

                }

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

     
        

    
        public ActionResult Delete(int id)
        {
            return View();
        }

     
    
    }
}
