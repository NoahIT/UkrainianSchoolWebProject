using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models
{
    public class ServiceUrlModel
    {
        public string orderReference { get; set; }
        public string status { get; set; }
        public string time { get; set; }
        public string signature { get; set; }
    }
}
