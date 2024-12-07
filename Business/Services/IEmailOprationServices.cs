using Abstraction.Interfaces;
using Abstraction.Queries;
using Azure;
using Domain.DTOs.Read;
using Domain.OptionDP;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class IEmailOperationServices : IEmailOperations
    {

        private readonly IStudentQueryRepository _studentQueryRepository;
        private readonly IOptions<EmailSettings> _emailSetting;     

        public IEmailOperationServices(IStudentQueryRepository studentQueryRepository, IOptions<EmailSettings> emailSetting)
        {
            _studentQueryRepository = studentQueryRepository;
            _emailSetting = emailSetting;
        }

        public async Task<EmailResponse> SendEmail(int studentId, CancellationToken cancellationToken = default)
        {
            var student = await _studentQueryRepository.GetStudentById(studentId, cancellationToken);

            if (string.IsNullOrWhiteSpace(student.Email))
                throw new ArgumentException("Student email address cannot be null or empty.");

            try
            {
                using (var smtpClient = new SmtpClient(_emailSetting.Value.Host.ToString(),_emailSetting.Value.Port )
                {
                    Credentials = new System.Net.NetworkCredential(_emailSetting.Value.SenderEmail, _emailSetting.Value.Password),
                    EnableSsl = true
                })
                using (var mailMessage = new MailMessage(_emailSetting.Value.SenderEmail, _emailSetting.Value.Password)
                {
                    Subject =_emailSetting.Value.Subject ,
                    Body = $"Dear {student.Name}, please click the confirm button",
                    IsBodyHtml = true
                })
                {
                    //// Wrap sending in Task.Run to support async
                    await Task.Run(() => smtpClient.Send(mailMessage), cancellationToken);


                    var response = new EmailResponse
                    {
                        IsSuccess = true,
                        IsConfirmed = true,
                        Message = "succes",
                        TransactionId = Guid.NewGuid()
                    };

                    return response;
                }
            }
            catch (Exception ex) 
            {
                var response = new EmailResponse
                {
                    IsSuccess = false,
                    IsConfirmed = false,
                    Message = ex.Message,
                    TransactionId = Guid.NewGuid()
                };

                return response;    

            }
        }
    }
}
