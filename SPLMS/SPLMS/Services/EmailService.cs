using Microsoft.AspNet.Identity;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace AuthenticationAPI.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendEmailasync(message);
        }
 
        private async Task configSendEmailasync(IdentityMessage message)
        {
            var emailMessage = new MailMessage();
            emailMessage.To.Add(message.Destination);
            emailMessage.From = new MailAddress("iit.splms@hotmail.com", "Software Project Lab");
            emailMessage.Subject = message.Subject;
            emailMessage.Body = message.Body;
            emailMessage.IsBodyHtml = true;

            await sendMail(emailMessage);
        }


        private async Task sendMail(MailMessage message)
        {
            var smtpClient = new SmtpClient();
            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["emailService:Account"],
                ConfigurationManager.AppSettings["emailService:Password"]);
            smtpClient.Credentials = credentials;
            smtpClient.Host = "smtp-mail.outlook.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(message);
        }
    }
}