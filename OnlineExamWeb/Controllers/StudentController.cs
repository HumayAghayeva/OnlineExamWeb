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
using OnlineExamWeb.Utilities;
using Domain.Entity.Write;
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
            IEmailOperations emailOperations, IAppLogger<StudentController> logger)
        {
            _commandRepository = commandRepository;
            _studentQueryRepository = studentQueryRepository;
            _fileManager = fileManager;
            _emailOperations = emailOperations;
            _logger = logger;
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
        public async Task<ActionResult> CreateStudent(CancellationToken cancellationToken)
        {  
            return View();
        }

        public async Task<IActionResult> CreateStudent(StudentRequestDTO studentRequestDTO, IFormCollection formCollection,
                CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(studentRequestDTO);
            }
            var uploadedPhoto = formCollection.Files["studentPhoto"];

            studentRequestDTO.Password= EncryptionHelper.Encrypt(studentRequestDTO.Password);

            studentRequestDTO.ConfirmPassword= EncryptionHelper.Encrypt(studentRequestDTO.ConfirmPassword);

            var student = await _commandRepository.AddStudent(studentRequestDTO, cancellationToken);

            if (!student.Success)
            {
                throw new Exception("The student was not added successfully.");
            }

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

            var emailResponse = await _emailOperations.SendEmail(student.Id, cancellationToken);

            if (!emailResponse.IsSuccess)
                throw new Exception("Failed to send the email notification.");
            else
               ViewBag.Message = "Your account has been created successfully!,Please check your email and click the confirmation link";
               return View(studentRequestDTO);
        }
    


        [HttpGet]
        public async Task<IActionResult> LoginStudent()
        {
            return  View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginStudent(StudentLoginDTO studentLoginDTO,CancellationToken cancellationToken)
        {
            studentLoginDTO.Password = EncryptionHelper.Encrypt(studentLoginDTO.Password);

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
