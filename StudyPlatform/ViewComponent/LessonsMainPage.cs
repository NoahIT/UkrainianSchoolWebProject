using Microsoft.AspNetCore.Mvc;

namespace StudyPlatform.ViewComponent
{
    public class LessonsMainPage : Microsoft.AspNetCore.Mvc.ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("LessonsMainPage");
        }
    }
}
