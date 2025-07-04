using Kurdemir.BL.ExternalServices.Abstractions;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Kurdemir.BL.Helpers;

namespace Kurdemir.BL.ExternalServices.Implements
{
    public class EmailService : IEmailService
    {
        readonly SmtpClient _client;
        readonly MailAddress _from;
        readonly HttpContext context;
        public EmailService(IOptions<SmtpOptions> option)
        {
            var opt = option.Value;
            _client = new SmtpClient(opt.Host, opt.Port);
            _client.Credentials = new NetworkCredential(opt.Username, opt.Password);
            _client.EnableSsl = true;
            _from = new MailAddress(opt.Username, "MREDU");
        }

        public void SendEmailConfirmation(string reciever, string name, int token)
        {
            MailAddress to = new(reciever);
            MailMessage message = new MailMessage(_from, to);
            message.Subject = "Confirm your email adress";
            message.Body = EmailTemplates.VerifyEmail;
            message.Body = EmailTemplates.VerifyEmail.Replace("__$name", name).Replace("__$CODE", token.ToString());
            message.IsBodyHtml = true;
            _client.Send(message);

        }
    }
}
