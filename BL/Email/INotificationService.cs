using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Email
{
    public interface INotificationService
    {
        Task SendEmailNoReply(MailMessageCustom custom);
        Task SendEmailSupport(MailMessageCustom custom);
        Task SendEmailInfo(MailMessageCustom custom);
    }
}
