using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.UserManager
{
    public interface ITeacherDAL
    {
        Task<Teacher?> GetTeacher(string abbr);
        Task<Teacher?> GetTeacher(string abbr, int? klass);
    }
}
