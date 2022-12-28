using codeKade.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace codeKade.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly IUserService _userService;
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        private readonly IBlogCommentService _blogCommentService;

        public HomeController(IUserService userService, IBlogService blogService, ICommentService commentService, IBlogCommentService blogCommentService)
        {
            _userService = userService;
            _blogService = blogService;
            _commentService = commentService;
            _blogCommentService = blogCommentService;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.CountOfTodayUsers = await _userService.GetTodayUsers();
            ViewBag.CountOfUsers = await _userService.CountOfUsers();

            ViewBag.CountOfTodayBlogs = await _blogService.CountOfTodayBlog();
            ViewBag.CountOfBlogs = await _blogService.CountOfBlogs();

            ViewBag.CountOfTodayEventComments = await _commentService.CountOfTodayComments();
            ViewBag.CountOfEventComments = await _commentService.CountOfComments();

            ViewBag.BlogCommentsToday = await _blogCommentService.CountOfTodayComments();
            return View();
        }
    }
}
