using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AppRole : Entity
    {
        public string Abbreviation { get; set; }
        public string RoleName { get; set; }
    }
}
