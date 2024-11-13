using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.UserManager;

namespace BL.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherDAL teacherDal;

        public TeacherService(ITeacherDAL teacherDal)
        {
            this.teacherDal = teacherDal;
        }

        public async Task<Teacher> GetTeacherByAbbr(string abbr, int? klass)
        {
            if (klass != null)
            {
                return await teacherDal.GetTeacher(abbr, klass) ?? new Teacher();
            }

            return await teacherDal.GetTeacher(abbr) ?? new Teacher();
        }
    }
}
