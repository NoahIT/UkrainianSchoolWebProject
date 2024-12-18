﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models
{
    public class UserGroup : Entity
    {
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
