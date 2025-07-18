using Abstraction;
using Abstraction.Command;
using Abstraction.Interfaces;
using Abstraction.Queries;
using Domain.DTOs.Read;
using Domain.DTOs.Write;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineExamWeb.Controllers;

namespace OnlineExamPlatform.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetStudent_ShouldReturnViewResult_WithStudent()
        {
            // Arrange

            var cancellationToken = CancellationToken.None;
            var studentId = 1007;
            var mockCommandRepository = new Mock<IStudentCommandRepository>();
            var mockQueryRepository = new Mock<IStudentQueryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockFileManager = new Mock<IFileManager>();
            var mockEmailOperations = new Mock<IEmailOperations>();
            var mockValidation = new Mock<IValidator<StudentRequestDto>>();
            var expectedStudent = new StudentResponseDto
            {
                WriteDBId = "1",
                Name = "Humay",
                LastName = "Aghayeva",
                FullName = "Aghayeva Humay",
                DateOfBirth = "28.10.1995",
                Email = "humayrza12@gmail.com",
                PIN = "6005N51",
                GroupName = "1"
            };
            // Mock the GetStudentById method to return the expected student
            mockQueryRepository
                .Setup(repo => repo.GetStudentById(studentId, cancellationToken))
                .ReturnsAsync(expectedStudent);

            // Inject the mocked repository into the controller
            var controller = new StudentController(mockCommandRepository.Object, mockQueryRepository.Object, mockFileManager.Object, mockEmailOperations.Object, mockValidation.Object,mockUnitOfWork.Object);


            // Act
            var result = await controller.GetStudent(studentId, cancellationToken);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<StudentResponseDto>(viewResult.Model);
            Assert.Equal(expectedStudent.Id, model.Id);
            Assert.Equal(expectedStudent.Name, model.Name);
        }
    }

}