using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.X509;

namespace DAL.Models
{
    public class Teacher : Entity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public string? Photo { get; set; }
        public string? Description { get; set; }
        public string? ExampleVideo { get; set; }
    }
}
