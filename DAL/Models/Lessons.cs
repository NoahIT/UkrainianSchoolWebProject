using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Lessons : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Teacher Teacher { get; set; }
        public int TeacherId { get; set; }
        public required DateTime DateOfLesson { get; set; }
        public string? Source { get; set; }

        public string TeacherName => this.Teacher?.User?.FirstName + " " + this.Teacher?.User?.LastName;
        public string Date => this.DateOfLesson.ToString("dd.MM.yyyy");
    }
}
