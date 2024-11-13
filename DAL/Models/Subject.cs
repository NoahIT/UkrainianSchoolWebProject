using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Subject : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AbbrSubject { get; set; }
        public GradeClass GradeClass { get; set; }
    }
}
