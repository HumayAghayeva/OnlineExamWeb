using Business.Repositories;
using Microsoft.AspNetCore.Mvc;
using Abstraction.Queries;
using Abstraction.Command;
using Domain.DTOs.Write;
using Domain.DTOs.Read;
using System.Threading;
using Domain.Entity;
using Business.Services;
namespace OnlineExamWeb.Controllers
{
    public class StudentController : Controller
    {
         private readonly IStudentCommandRepository _commandRepository;
         private readonly IStudentQueryRepository _studentQueryRepository;
         private readonly IEmailOperationServices _emailOperationServices;
         private readonly IConfigureImageServices _configureImageServices;   

        public StudentController(IStudentCommandRepository commandRepository,
            IStudentQueryRepository studentQueryRepository,
            IConfigureImageServices configureImageServices,
            IEmailOperationServices emailOperationServices)
        {
          _commandRepository = commandRepository;
          _studentQueryRepository = studentQueryRepository;
          _configureImageServices = configureImageServices; 
          _emailOperationServices = emailOperationServices;
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

                    var fileResponse = await _configureImageServices.ConfigureImage(uploadedPhoto);

                    var studentPhotoDto = new StudentPhotoDTO
                    {
                        PhotoPath = fileResponse.FilePath,
                        StudentId = student.Id,
                        FileName = fileResponse.FileName
                    };

                }

                var emailResponse=await _emailOperationServices.SendEmail(student.Id, cancellationToken);

                if (emailResponse.IsConfirmed == true)
                {

                    return RedirectToAction(nameof(GetStudent));
                }
                else
                {
                    return RedirectToAction(nameof(CreateStudent));
                }
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
