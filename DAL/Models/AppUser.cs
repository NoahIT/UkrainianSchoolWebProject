using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AppUser : Entity
    {
        public string Email { get; set; } = null!;
        public string? Login { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? FirstPassword { get; set; }
        public string? Salt { get; set; }

    }
}
