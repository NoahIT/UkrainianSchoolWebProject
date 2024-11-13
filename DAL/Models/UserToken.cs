using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public class UserToken
    {
        [Key]
        public Guid UserTokenId { get; set; }

        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
