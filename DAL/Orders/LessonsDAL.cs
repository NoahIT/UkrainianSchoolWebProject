using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Orders
{
    public class LessonsDAL : ILessonsDAL
    {
        private readonly PlatformDbContext context;
        private readonly IOrderDAL orderDal;

        public LessonsDAL(PlatformDbContext context, IOrderDAL orderDal)
        {
            this.context = context;
            this.orderDal = orderDal;
        }

        public async Task<List<Lessons>> GetAllLessonsByAbbr(string abbr)
        {
            return await context.Lessons.Where(x=>x.Teacher.Subject.AbbrSubject == abbr).ToListAsync();
        }

        public async Task<List<Lessons>> GetAllLessonsByAbbr(int klass, string abbr, int PageSize, int pageIndex)
        {
            return await context.Lessons
                .Where(x=>x.Teacher.Subject.AbbrSubject == abbr && x.Teacher.Subject.GradeClass == (GradeClass)klass)
                .Skip(pageIndex * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }

        public async Task<List<Lessons>> GetAllLessonsByAbbr(string abbr, int PageSize, int pageIndex)
        {
            return await context.Lessons
                .Where(x => x.Teacher.Subject.AbbrSubject == abbr)
                .Skip(pageIndex * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }

        public async Task<List<Lessons>> GetAllLessonsByAbbrAndValid(int userId, string abbr, int pageSize, int currIndex)
        {
            var subjectIds = (await orderDal.GetApprovedOrders(userId))?
                .SelectMany(s => s.Subjects.Where(x => x.Subject.AbbrSubject == abbr))
                .Select(x => x.Subject.Id)
                .ToList();

            if (subjectIds == null)
            {
                return new List<Lessons>();
            }


            return await context.Lessons
                .Where(x => subjectIds.Contains(x.Teacher.SubjectId))
                .Skip(currIndex * pageSize)
                .Take(pageSize)
                .Select(x=>new Lessons()
                {
                    Name = x.Name,
                    Description = x.Description,
                    DateOfLesson = x.DateOfLesson,
                    Source = x.Source,
                }).ToListAsync();
        }

        public async Task<List<Lessons>> GetAllLessonsByAbbrAndValid(int klass, int userId, string abbr, int pageSize, int currIndex)
        {
            var subjectIds = (await orderDal.GetApprovedOrders(userId))?
                .SelectMany(s => s.Subjects.Where(x => x.Subject.AbbrSubject == abbr && x.Subject.GradeClass == (GradeClass)klass))
                .Select(x => x.Subject.Id)
                .ToList();

            if (subjectIds == null)
            {
                return new List<Lessons>();
            }


            return await context.Lessons
                .Where(x => subjectIds.Contains(x.Teacher.SubjectId))
                .Skip(currIndex * pageSize)
                .Take(pageSize)
                .Select(x => new Lessons()
                {
                    Name = x.Name,
                    Description = x.Description,
                    DateOfLesson = x.DateOfLesson,
                    Source = x.Source,
                }).ToListAsync();

        }
    }
}
