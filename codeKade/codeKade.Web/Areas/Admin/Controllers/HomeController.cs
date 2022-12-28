using codeKade.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace codeKade.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }


        public IActionResult Index()
        {
            ViewBag.CountOfTodayUsers = _userService.GetTodayUsers();
            ViewBag.CountOfUsers = _userService.CountOfUsers();
            return View();
        }
    }
}
