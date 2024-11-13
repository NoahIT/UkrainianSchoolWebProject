using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; } = "Name";
        public string LastName { get; set; } = "Surname";
        public string Photo { get; set; } = "default";

        public int? AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual List<UserGroup> Groups { get; set; }
    }
}
