using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.UserManager
{
    public class TeacherDAL : ITeacherDAL
    {
        private readonly PlatformDbContext context;

        public TeacherDAL(PlatformDbContext context)
        {
            this.context = context;
        }

        public async Task<Teacher?> GetTeacher(string abbr)
        {
            return await context.Teachers
                .Select(x=>new Teacher()
                {
                    Description = x.Description,
                    User = new User()
                    {
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName
                    },
                    ExampleVideo = x.ExampleVideo,
                    Photo = x.Photo,
                    Subject = x.Subject

                })
                .FirstOrDefaultAsync(x => x.Subject.AbbrSubject == abbr);
        }

        public async Task<Teacher?> GetTeacher(string abbr, int? klass)
        {
            return await context.Teachers
                .Select(x => new Teacher()
                {
                    Description = x.Description,
                    User = new User()
                    {
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName
                    },
                    ExampleVideo = x.ExampleVideo,
                    Photo = x.Photo,
                    Subject = x.Subject

                })
                .FirstOrDefaultAsync(x => x.Subject.AbbrSubject == abbr && x.Subject.GradeClass == (GradeClass)klass);
        }
    }
}
