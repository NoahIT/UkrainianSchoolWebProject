using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Extensions.Options;

namespace DAL.Helpers
{
    public class EmailHelper : IEmailHelper
    {
        private readonly EmailSettings _emailSettings;

        public EmailHelper(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public SmtpClient GetSmptClient()
        {
            SmtpClient client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword),
                EnableSsl = true
            };

            return client;
        }

        public MailMessage ToMailMessage(MailMessageCustom custom, string from)
        {
            var m = new MailMessage()
            {
                From = new MailAddress(from, "Uniqum School"),
                Subject = $"{custom.Subject}",
                Body = CreateEmailBody(custom.Body),
                IsBodyHtml = true
            };

            m.Sender = new MailAddress(from);
            
            m.To.Add(custom.ToEmail);

            m.Headers.Add("X-Priority", "3");
            m.Headers.Add("X-MSMail-Priority", "Normal");
            m.Headers.Add("X-Mailer", "Microsoft Outlook Express 6.00.2900.2869");
            m.Headers.Add("X-MimeOLE", "Produced By Microsoft MimeOLE V6.00.2900.2869");
            m.Headers.Add("Return-Path", from);

            return m;
        }

        private string CreateEmailBody(string custom)
        {
            return $@"
                <html>
                <body>
                    {custom}
                </body>
                </html>";
        }

    }
}
