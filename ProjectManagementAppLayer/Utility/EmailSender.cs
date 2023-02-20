using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_config["Email:personal_Email"],
                "tvvzlgptsbblivzw")
            };
            
            return client.SendMailAsync(
                new MailMessage(from: _config["Email:personal_Email"],
                                to: email,
                                subject,
                                htmlMessage
                                ));
        }
    }
}
