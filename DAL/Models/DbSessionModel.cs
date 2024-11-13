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
    [PrimaryKey(nameof(DbSessionId),nameof(UserId))]
    public class DbSessionModel
    {
        public Guid DbSessionId { get; set; }

        public string? SessionData { get; set; }

        
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime LastAccessed { get; set; } = DateTime.UtcNow;
    }
}
