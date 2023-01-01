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

        [HttpGet]
        public async Task<IActionResult> GetUserDetail(long id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "User", new { area = "Admin" });
            }
            return PartialView("_UserDetail", await _userService.GetById(id));
        }

        public async Task<IActionResult> AddUser()
        {
            return PartialView("_AddUser");
        }

        public async Task<IActionResult> EditUser(long id)
        {
            var user = await _userService.GetById(id);
            var editDetail = new EditProfileDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Mobile = user.Mobile,
                Id = user.ID
            };
            return PartialView("_EditUser", editDetail);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditProfileDTO edit)
        {
            var res = await _userService.EditUser(edit);
            if (res)
            {
                TempData[SuccessMessage] = "اطلاعات با موفقیت ویرایش شد";
            }

            return RedirectToAction("Index", "User", new { area = "Admin" });
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterUserDTO register)
        {
            var res = await _userService.AdminRegisterUser(register);
            if (res == RegisterUserResult.EmailConflict)
            {
                TempData[ErrorMessage] = "ایمیل وارد شده تکراری میباشد";
                return RedirectToAction("Index", "User", new { area = "Admin" });
            }

            TempData[SuccessMessage] = "کاربر جدید با موفقیت افزوده شد";
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }

        public async Task<IActionResult> DeleteUser(long id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "User", new { area = "Admin" });
            }

            var res = await _userService.DeleteUser(id);
            if (res == 0)
            {
                return RedirectToAction("Index", "User", new { area = "Admin" });
            }
            else
            {
                TempData[SuccessMessage] = "کاربر با موفقیت حذف شد";
                return Json(id);
            }
        }

        public async Task<IActionResult> ChangePermission(long id)
        {
            var res = await _userService.ChangePermission(id);
            if (res)
            {
                TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                return RedirectToAction("Index", "User", new { area = "Admin" });
            }

            TempData[ErrorMessage] = "مشکلی در انجام فرایند پیش آمد";
            return RedirectToAction("Index", "User", new { area = "Admin" });
        }

        public async Task<IActionResult> TodayUsers(FilterUserDTO filter)
        {
            var users = await _userService.GetTodayUsers(filter);
            return View(users);
        }
    }
}
