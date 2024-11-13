using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;

namespace BL.Email
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailSenderDAL senderDAL;

        public NotificationService(IEmailSenderDAL senderDal)
        {
            senderDAL = senderDal;
        }

        public async Task SendEmailNoReply(MailMessageCustom custom)
        {
            await senderDAL.SendMail(custom, "noreply@uniqum.school");
        }

        public async Task SendEmailSupport(MailMessageCustom custom)
        {
            await senderDAL.SendMail(custom, "support@uniqum.school");
        }

        public async Task SendEmailInfo(MailMessageCustom custom)
        {
            await senderDAL.SendMail(custom, "info@uniqum.school");
        }
    }


}
