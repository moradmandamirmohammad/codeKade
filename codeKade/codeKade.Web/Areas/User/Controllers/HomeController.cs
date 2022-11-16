using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.DTOs.Account;
using codeKade.Web.HttpContext;
using Microsoft.AspNetCore.Mvc;

namespace codeKade.Web.Areas.User.Controllers
{
    public class HomeController : UserBaseController
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetById(User.GetUserID());
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileDTO Users)
        {
            Users.Id = User.GetUserID();
            var res = await _userService.EditUser(Users);
            if (res)
            {
                TempData[SuccessMessage] = "اطلاعات با موفقیت بروزرسانی شد";
            }
            return RedirectToAction("Index", "Home", new { Area = "User" });
        }
    }
}
