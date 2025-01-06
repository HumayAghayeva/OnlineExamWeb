using Moq;
using NuGet.ContentModel;
using OnlineExamWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.DTOs.Write;
using OnlineExamWeb.Utilities;
using Domain.DTOs.Read;
using Microsoft.AspNetCore.Mvc;
using Abstraction.Command;
using Abstraction.Interfaces;
using Abstraction.Queries;

namespace OnlineExamPlatform.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [Priority(1)]
        public async Task LoginStudentTest(CancellationToken cancellationToken)
        {
            // Arrange
            var mockCommandRepository = new Mock<IStudentCommandRepository>(); 
            var mockQueryRepository = new Mock<IStudentQueryRepository>();
            var mockFileManager = new Mock<IFileManager>();
            var mockEmailOperations = new Mock<IEmailOperations>();
            var mockLogger = new Mock<IAppLogger<StudentController>>();
            var encryptedPassword = EncryptionHelper.Encrypt("Test");

            var studentDto = new StudentLoginDTO
            {
                Email = "humayrza12@gmail.com",
                Password = encryptedPassword
            };

            var studentResponse = new StudentResponseDTO
            {
                Id = 1,
                Name = "Humay",
                LastName = "Aghayeva",
                FullName = "Aghayeva Humay",
                DateOfBirth = "28.10.1995",
                Email = "humayrza12@gmail.com",
                PIN = "6005N51",
                GroupName = "1"
            };

            // Mock the repository method
            mockCommandRepository
                .Setup(repo => repo.LoginStudent(studentDto, It.IsAny<CancellationToken>()))
                .ReturnsAsync(studentResponse);

            // Inject the mocked repository into the controller
            var controller = new StudentController( mockCommandRepository.Object, mockQueryRepository.Object,  mockFileManager.Object,  mockEmailOperations.Object, mockLogger.Object );

            // Act
            var result = await controller.LoginStudent(studentDto, CancellationToken.None) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual(nameof(StudentController.GetStudent), result.ActionName, "Should redirect to GetStudent.");

            Assert.AreEqual(studentResponse.Id, result.RouteValues["studentId"], "StudentId in route values should match.");

        }
    }
}
