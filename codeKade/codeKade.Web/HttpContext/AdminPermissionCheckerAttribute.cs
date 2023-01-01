using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using codeKade.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace codeKade.Web.HttpContext
{
    public class AdminPermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IUserService _userService;
        
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId =  Convert.ToInt64(context.HttpContext.User.GetUserClaimByType(HttpContextExtentions.CustomClaimType.NameIdentifier));
                var IsAdmin = _userService.IsUserAdmin(userId).Result;
                if (!IsAdmin)
                {
                    context.Result = new RedirectResult("/");
                }
                
            }
        }
    }
}
