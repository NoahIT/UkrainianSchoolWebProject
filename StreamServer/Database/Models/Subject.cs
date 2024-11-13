using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace StreamServer.Database.Models
{
    public class Subject : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AbbrSubject { get; set; }
        public GradeClass GradeClass { get; set; }
    }
    public enum GradeClass
    {
        Class10 = 10,
        Class11 = 11
    }
}
