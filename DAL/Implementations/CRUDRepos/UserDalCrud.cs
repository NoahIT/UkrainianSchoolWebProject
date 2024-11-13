using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Implementations.CRUDRepos
{
    public class UserDalCrud : BaseRepository<User>
    {
        public UserDalCrud(PlatformDbContext context) : base(context) { }
    }
}
