using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.Recovery
{
    public interface IRecovery
    {
        Task SendToEmailPassVerifLink(string email);

        Task<string> GetEmailByCode(string code);

        Task ChangePassword(string p1, string p2, string code);
    }
}
