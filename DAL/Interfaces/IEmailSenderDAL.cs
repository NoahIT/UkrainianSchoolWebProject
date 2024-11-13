using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IEmailSenderDAL
    {
        Task SendMail(MailMessageCustom message, string emailFrom);
    }
}