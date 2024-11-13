using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PasswordCodes : Entity
    {
        public Guid VerificationCode { get; set; } = Guid.NewGuid();

        public virtual User User { get; set; }

    }
}
