using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using StudyPlatform.Models;
using System.Diagnostics;
using BL.Services;
using DAL.Models;

namespace StudyPlatform.Controllers
{
    public class ClassesController : Controller
    {
        private readonly ITeacherService teacherService;
        private readonly List<string> validSubjects = new List<string>()
        {
            "math",
            "history",
            "language"
        };

        public ClassesController(ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }

        [HttpGet]
        [Route("/classes")]
        public IActionResult Classes()
        {
            return View("Classes");
        }

        [HttpGet]
        [Route("/selectedClass")]
        public async Task<IActionResult> SelectedClass(int? klass, [Required]string s)
        {
            if (!validSubjects.Contains(s))
            {
                return Redirect("/");
            }

            Teacher? teacher = await teacherService.GetTeacherByAbbr(s, klass);


            return View("SelectedClass", teacher);
        }
    }
}
