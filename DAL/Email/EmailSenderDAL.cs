using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Email
{
    public class EmailSenderDAL(IEmailHelper emailHelper) : IEmailSenderDAL
    {
        private readonly IEmailHelper emailHelper = emailHelper;

        public async Task SendMail(MailMessageCustom message,string emailFrom)
        {
            var client = emailHelper.GetSmptClient();

            var mailMessage = emailHelper.ToMailMessage(message, emailFrom);

            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
