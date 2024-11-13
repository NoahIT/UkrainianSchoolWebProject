using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Review : Entity
    {
        public string FullName { get; set; }

        public string Description { get; set; }
    }
}
