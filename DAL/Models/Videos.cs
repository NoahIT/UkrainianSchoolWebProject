using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Videos : Entity
    {
        public string Path { get; set; }
        public string Name { get; set; }

        public string Where { get; set; }
    }
}
