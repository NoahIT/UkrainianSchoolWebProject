using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    [PrimaryKey(nameof(AppUserId),nameof(AppRoleId))]
    public class AppUserAppRole
    {
        public int? AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public int? AppRoleId { get; set; }
        public virtual AppRole AppRole { get; set; }
    }
}
