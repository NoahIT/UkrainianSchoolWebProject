using Microsoft.AspNetCore.Mvc;
using StudyPlatform.ViewModels;

namespace StudyPlatform.ViewComponent
{
    public class ChatViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(StreamViewModel streamView)
        {
            return View("Chat", streamView);
        }
    }
}
