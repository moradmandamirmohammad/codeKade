using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.DTOs.Account;
using Microsoft.AspNetCore.Mvc;

namespace codeKade.Web.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index(FilterUserDTO filter)
        {
            return View(await _userService.GetUsersList(filter));
        }
    }
}
