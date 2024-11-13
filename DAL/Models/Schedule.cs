using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.X509;

namespace DAL.Models
{
    public class Schedule : Entity
    {
        public string Day { get; set; }
        public string Time { get; set; }

        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
