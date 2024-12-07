using Business.Repositories;
using Microsoft.AspNetCore.Mvc;
using Abstraction.Queries;
using Abstraction.Command;
using Domain.DTOs.Write;
using Domain.DTOs.Read;
using System.Threading;
using Domain.Entity;
using Business.Services;
using Abstraction.Interfaces;
using Domain.Entity.Read;
namespace OnlineExamWeb.Controllers
{
    public class StudentController : Controller
    {
         private readonly IStudentCommandRepository _commandRepository;
         private readonly IStudentQueryRepository _studentQueryRepository;
         private readonly IAppLogger<StudentController> _logger;    
         private readonly IEmailOperations _emailOperations;
         private readonly IFileManager _fileManager;   

        public StudentController(IStudentCommandRepository commandRepository,
            IStudentQueryRepository studentQueryRepository,
            IFileManager fileManager,
            IEmailOperations emailOperations,IAppLogger<StudentController> logger)
        {
          _commandRepository = commandRepository;
          _studentQueryRepository = studentQueryRepository;
          _fileManager = fileManager;
          _emailOperations = emailOperations;
         _logger= logger;
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

        [HttpGet]
        public ActionResult CreateStudent()
        {
            return View();
        }

        public async Task<IActionResult> CreateStudent(
      StudentRequestDTO studentRequestDTO,
      IFormCollection formCollection,
      CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(studentRequestDTO);
            }

            try
            {
                // Upload photo from the form
                var uploadedPhoto = formCollection.Files["studentPhoto"];

                // Add student to the database
                var student = await _commandRepository.AddStudent(studentRequestDTO, cancellationToken);
                if (!student.Success)
                {
                    throw new Exception("The student was not added successfully.");
                }

                // If a photo is uploaded, process it
                if (uploadedPhoto != null && uploadedPhoto.Length > 0)
                {
                    var fileResponse = await _fileManager.ConfigureImage(uploadedPhoto);
                    if (fileResponse == null || string.IsNullOrEmpty(fileResponse.FilePath))
                    {
                        throw new Exception("Failed to process the uploaded photo.");
                    }

                    var studentPhotoDto = new StudentPhotoDTO
                    {
                        PhotoPath = fileResponse.FilePath,
                        StudentId = student.Id,
                        FileName = fileResponse.FileName
                    };

                    var photoResult = await _commandRepository.AddStudentPhoto(studentPhotoDto, cancellationToken);
                    if (!photoResult.Success)
                    {
                        throw new Exception("The photo was not added successfully.");
                    }
                }

                // Send email notification
                var emailResponse = await _emailOperations.SendEmail(student.Id, cancellationToken);
                if (!emailResponse.IsConfirmed)
                {
                    throw new Exception("Failed to send the email notification.");
                }

                TempData["SuccessMessage"] = "Student created successfully!";
                return RedirectToAction("Index"); // Redirect to a success page or list view
            }
            catch (Exception ex)
            {
                // Log the exception (logging not shown for simplicity)
                TempData["ErrorMessage"] = $"An error occurred while creating the student: {ex.Message}";
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
