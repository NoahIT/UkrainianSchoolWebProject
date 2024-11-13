using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.Services
{
    public interface ITeacherService
    {
        Task<Teacher> GetTeacherByAbbr(string abbr,int? klass);
    }
}
