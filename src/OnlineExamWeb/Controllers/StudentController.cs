using Business.Repositories;
using Microsoft.AspNetCore.Mvc;
using Abstraction.Queries;
using Abstraction.Command;
using Domain.DTOs.Write;
using Domain.DTOs.Read;
using System.Threading;
using Business.Services;
using Abstraction.Interfaces;
using OnlineExamWeb.Utilities;
using Domain.Entity;
using FluentValidation;
using System;
using Serilog;
using Abstraction;
using Domain.Dtos.Write;
using Domain.Enums;
using AutoMapper;

namespace OnlineExamWeb.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentCommandRepository _commandRepository;
        private readonly IStudentQueryRepository _studentQueryRepository;
        private IValidator<StudentRequestDto> _studentValidator;
        private readonly IEmailOperations _emailOperations;
        private readonly IFileManager _fileManager;
        private readonly JwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public StudentController(IStudentCommandRepository commandRepository,
            IStudentQueryRepository studentQueryRepository,
            IFileManager fileManager,
            IEmailOperations emailOperations, 
            IValidator<StudentRequestDto> studentValidator,IUnitOfWork unitOfWork , 
            JwtTokenService jwtTokenService,  IMapper mapper)
        {
            _commandRepository = commandRepository;
            _studentQueryRepository = studentQueryRepository;
            _fileManager = fileManager;
            _emailOperations = emailOperations;
            _studentValidator = studentValidator;
            _unitOfWork = unitOfWork;   
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        public ActionResult Index(StudentResponseDto student)
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

        public async Task<IActionResult> CreateStudent(StudentRequestDto studentRequestDto, IFormCollection formCollection,
                CancellationToken cancellationToken)
        {
            try
            {
                // Start a transaction
                await _unitOfWork.BeginTransactionAsync(cancellationToken: cancellationToken);

                var studentvalidation = await _studentValidator.ValidateAsync(studentRequestDto);

                if (!studentvalidation.IsValid)
                {
                    ViewBag.Message = studentvalidation.Errors.FirstOrDefault();

                    return View(studentRequestDto);
                }

                var uploadedPhoto = formCollection.Files["studentPhoto"];

                studentRequestDto.Password = EncryptionHelper.Encrypt(studentRequestDto.Password);

                studentRequestDto.ConfirmPassword = EncryptionHelper.Encrypt(studentRequestDto.ConfirmPassword);

                var student = await _commandRepository.AddStudent(studentRequestDto, cancellationToken);

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

                    var StudentPhotoDto = new StudentPhotoDto
                    {
                        PhotoPath = fileResponse.FilePath,
                        StudentId = student.Id,
                        FileName = fileResponse.FileName
                    };

                    var photoResult = await _commandRepository.AddStudentPhoto(StudentPhotoDto, cancellationToken);

                    if (!photoResult.Success)
                    {
                        throw new Exception("The photo was not added successfully.");
                    }
                }
                // Save changes
                await _unitOfWork.SaveAsync(cancellationToken);

                var emailResponse = await _emailOperations.SendEmail(student.Id, cancellationToken);

                if (!emailResponse.IsSuccess)
                    throw new Exception("Failed to send the email notification.");
                // Commit transaction
                await _unitOfWork.CommitAsync(cancellationToken);

                ViewBag.Message = "Your account has been created successfully! Please check your email and click the confirmation link.";
                return View(studentRequestDto);
            }
            
            catch (Exception ex) {
                // Rollback transaction on failure
                await _unitOfWork.RollbackAsync(cancellationToken);

                // Log exception (add proper logging)
                ViewBag.Message = ex.Message;
                return View(studentRequestDto);
            }
         }
    


        [HttpGet]
        public async Task<IActionResult> LoginStudent()
        {
            return  View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginStudent(StudentLoginDto studentLoginDto, CancellationToken cancellationToken)
        {
            var encryptedPassword = EncryptionHelper.Encrypt(studentLoginDto.Password);
          
            studentLoginDto.Password = encryptedPassword;

            var studentResponse = await _commandRepository.LoginStudent(studentLoginDto, cancellationToken);

            if (studentResponse == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            var studentRoleDto = new  StudentRolesDto
            {
                StudentId= Convert.ToInt32(studentResponse.WriteDBId),
                RoleId= (int)Roles.QuizParticipant,
                CreateDate=DateTime.Now
            };

             
           
            var roleResult= await _commandRepository.AssignRoleToStudentAsync(studentRoleDto, cancellationToken);

            if (!roleResult.Success)
            {
                return BadRequest("Failed to assign student role.");
            }
                    
            return RedirectToAction(nameof(GetStudent), new { studentId = studentResponse.WriteDBId });
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
