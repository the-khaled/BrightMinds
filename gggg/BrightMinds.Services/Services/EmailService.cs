using BrightMinds.Services.Helpers.Emails;
using BrightMinds.Services.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
           _configuration = configuration;
        }
        public async Task SendEmail(IEmailStructure emailStructure)
        {

            string body = emailStructure.Body;


           // string body = $"<h2>hello {displayName}</h2><p>your confirm email is<br/>{confirmLink} </p> ";
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(emailStructure.To));
            email.Subject = emailStructure.Subject;

            // email.Body = new TextPart(TextFormat.Html) { Text = body };
            var builder = new BodyBuilder();
           
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();

            using var semtp = new SmtpClient();
           await semtp.ConnectAsync(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
           await semtp.AuthenticateAsync(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
           await semtp.SendAsync(email);
          await  semtp.DisconnectAsync(true);

        }
    }
}
