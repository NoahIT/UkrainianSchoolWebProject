using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Orders;

namespace BL.Services
{
    public class LessonsService : ILessonsService
    {
        private readonly IUserService _userService;
        private readonly ILessonsDAL _lessonsDal;

        public LessonsService(IUserService userService, ILessonsDAL lessonsDal)
        {
            _userService = userService;
            _lessonsDal = lessonsDal;
        }

        public async Task<List<Lessons>> GetLessonsByAbbrAndValid(string abbr,int pageSize, int index)
        {
            var userId = await _userService.GetUserId();

            if (userId==null)
            {
                return new List<Lessons>();
            }

            var lessons =  await _lessonsDal.GetAllLessonsByAbbrAndValid((int)userId, abbr, pageSize, index);

            return lessons;
        }

        public async Task<List<Lessons>> GetLessonsByAbbrAndValid(int klass, string abbr, int pageSize, int index)
        {
            var userId = await _userService.GetUserId();

            if (userId == null)
            {
                return new List<Lessons>();
            }

            var lessons = await _lessonsDal.GetAllLessonsByAbbrAndValid(klass,(int)userId, abbr, pageSize, index);

            return lessons;
        }
    }
}
