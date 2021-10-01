using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace GerenciamentoComercio_Domain.Utils.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly string _emailSender = "noreply@latech-erp.com";
        private readonly string _emailPassword = "Lealdrade123@";
        private readonly string _provider = "webmail.latech-erp.com";
        private readonly string _smptPort = "587";

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        public EmailSender(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public void Send(string[] emails, string subject, string message)
        {
            string destinationEmails = string.Join(";", emails);
            CallSendEmail(destinationEmails, subject, message);
        }
        public void Send(string email, string subject, string message)
        {
            CallSendEmail(email, subject, message);
        }

        private void CallSendEmail(string destinationEmail, string subject, string message)
        {
            var emailSenderInfo = new EmailSenderInfo
            {
                DestinationEmail = destinationEmail,
                Subject = subject,
                Message = message
            };

            Thread thread = new Thread(new ParameterizedThreadStart(SendEmail));
            thread.Start(emailSenderInfo);
        }

        private void SendEmail(object param)
        {
            EmailSenderInfo emailSenderInfo = (EmailSenderInfo)param;

            if (_hostingEnvironment.IsDevelopment())
            {
                if (string.IsNullOrEmpty(_configuration["EmailSandbox"])) return;
                emailSenderInfo.DestinationEmail = _configuration["EmailSandbox"];
            }
            else if (_hostingEnvironment.IsStaging())
            {
                string defaultDestination = _configuration.GetSection("DefaultDestinationEmail").Value;
                if (string.IsNullOrEmpty(defaultDestination)) return;
                emailSenderInfo.DestinationEmail = defaultDestination;
            }

            if (string.IsNullOrEmpty(emailSenderInfo.DestinationEmail))
                throw new ArgumentNullException(nameof(emailSenderInfo.DestinationEmail));

            try
            {
                EmailConfig.EmailConfig.SendEmail(_emailSender,
                                        emailSenderInfo.DestinationEmail,
                                        emailSenderInfo.Subject,
                                        emailSenderInfo.Message,
                                        _provider,
                                        _smptPort,
                                        _emailSender,
                                        _emailPassword,
                                        string.Empty,
                                        true);
            }
            catch(Exception ex)
            {
            }
        }
    }
}
