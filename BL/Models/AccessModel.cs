using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class AccessModel
    {
        public bool IsLoggedIn { get; set; } = false;
        public bool IsStudent { get; set; } = false;
        public bool IsSubscription { get; set; } = false;
        public bool IsTeacher { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
    }
}
