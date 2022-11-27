using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.DTOs.Blog;
using Microsoft.AspNetCore.Mvc;

namespace codeKade.Web.Controllers
{
    public class BlogController : SiteBaseController
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCommentService _blogCommentService;

        public BlogController(IBlogService blogService, IBlogCommentService blogCommentService)
        {
            _blogService = blogService;
            _blogCommentService = blogCommentService;
        }

        public async Task<IActionResult> Index(FilterBlogDTO filter)
        {
            var data = await _blogService.GetAll(filter);
            return View(data);
        }

        public async Task<IActionResult> Details(long id)
        {
            if (id == 0)
            {
                TempData[ErrorMessage] = "مقاله مورد نظر یافت نشد";
                return Redirect("/");
            }
            await _blogService.AddSeenToBlog(id);
            ViewBag.Comments = await _blogCommentService.GetBlogComments(id);
            ViewBag.MostSeenBlog = await _blogService.GetMostSeenBlog();
            ViewBag.Categories = await _blogService.GetCategories();
            var data = await _blogService.GetBlogDetail(id);
            return View(data);
        }

        public async Task<IActionResult> AddComment()
    }
}
