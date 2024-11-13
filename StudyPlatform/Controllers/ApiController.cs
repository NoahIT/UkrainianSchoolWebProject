using BL.Models;
using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudyPlatform.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly IUserService userService;
        private readonly ILessonsService lessonsService;
        private const int PageSize = 3;


        private readonly List<string> validSubjects = new List<string>()
        {
            "math",
            "history",
            "language"
        };

        public ApiController(IUserService userService, ILessonsService lessonsService)
        {
            this.userService = userService;
            this.lessonsService = lessonsService;
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        [Route("get-lessons")]
        public async Task<JsonResult> GetLessons(int pageIndex = 0)
        {
            string[] Images = { "~/Images/HelloUserPage/Ellipse 51.png",
                "~/Images/HelloUserPage/Ellipse 54.png",
                "~/Images/HelloUserPage/Ellipse 53.png" };
            Random r = new Random();

            var lessons = await userService.GetLessonsByValidSubscription();

            if (lessons == null)
            {
                return Json(null);
            }

            var lessonsDto = lessons
                .Skip(pageIndex * PageSize)
                .Take(PageSize)
                .Select(lesson => new LessonDto
                {
                    Name = lesson.Name,
                    Description = lesson.Description,
                    TeacherName = lesson.TeacherName,
                    Date = lesson.Date,
                    Image = Url.Content(Images[r.Next(0, 2)])
                }).ToList();

            return Json(lessonsDto);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        [Route("get-lessons-abbr")]
        public async Task<JsonResult> GetLessonsByAbbr(int? klass,string abbr, int pageIndex = 0)
        {
            if (!validSubjects.Contains(abbr))
            {
                return Json(null);
            }

            List<Lessons>? lessons = await userService.GetAllLessonsByAbbr(klass, abbr, PageSize, pageIndex);


            if (lessons == null)
            {
                return Json(null);
            }

            var lessonsDto = lessons
                .Select(x => new
                {
                    name = x.Name,
                    teacherName = x.TeacherName,
                    date = x.Date
                }).ToList();

            return Json(lessonsDto);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        [Route("get-lessons-abbr-valid")]
        public async Task<JsonResult> GetLessonByAbbrAndValid(int? klass,string abbr, int pageIndex = 0)
        {
            if (!validSubjects.Contains(abbr))
            {
                return Json(null);
            }

            List<Lessons> lessons;

            if (klass == null)
            {
                lessons = await lessonsService.GetLessonsByAbbrAndValid(abbr, PageSize, pageIndex);
            }
            else
            {
                List<int> validKlass = new List<int>() { 10, 11 };
                if (!validKlass.Contains((int)klass))
                {
                    return Json(null);
                }

                lessons = await lessonsService.GetLessonsByAbbrAndValid((int)klass, abbr, PageSize, pageIndex);
            }

            var view = Json(lessons);

            return view;
        }

    }
}
