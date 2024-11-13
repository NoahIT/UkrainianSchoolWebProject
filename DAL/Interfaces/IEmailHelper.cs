using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IEmailHelper
    {
        public SmtpClient GetSmptClient();

        public MailMessage ToMailMessage(MailMessageCustom custom, string From);
    }
}
