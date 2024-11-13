using BL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudyPlatform.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class SiteAuthorize : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            IUserService? service = context.HttpContext.RequestServices.GetService<IUserService>();
            if (service == null)
            {
                throw new Exception("No user Middleware");
            }

            int? userId = await service.GetUserId();


            if (userId == null)
            {
                context.Result = new RedirectResult("/login");
                return;
            }
        }
    }
}
