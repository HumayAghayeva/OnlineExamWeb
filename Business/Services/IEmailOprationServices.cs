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
        private readonly IOptions<SenderEmail> _senderEmail;
        private readonly string _host;
        private readonly int _port;
        private readonly string _recipientEmail;
        private readonly string _body;
        private readonly string _subject;

        public IEmailOperationServices(IStudentQueryRepository studentQueryRepository, IOptions<SenderEmail> senderEmail,
            string host, int port,
            string recipientEmail,
            string body,
            string subject)
        {
            _host = host;
            _port = port;
            _recipientEmail = recipientEmail;
            _subject = subject;
            _body = body;
            _studentQueryRepository = studentQueryRepository;
            _senderEmail = senderEmail;
        }

        public async Task<EmailResponse> SendEmail(int studentId, CancellationToken cancellationToken = default)
        {
            var student = await _studentQueryRepository.GetStudentById(studentId, cancellationToken);

            if (string.IsNullOrWhiteSpace(student.Email))
                throw new ArgumentException("Student email address cannot be null or empty.");

            try
            {
                using (var smtpClient = new SmtpClient(_host, _port)
                {
                    Credentials = new System.Net.NetworkCredential(_senderEmail.Value.Email, _senderEmail.Value.Password),
                    EnableSsl = true
                })
                using (var mailMessage = new MailMessage(_senderEmail.Value.Email, student.Email)
                {
                    Subject = "Welcome to Our OnlineExamPlatform",
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
