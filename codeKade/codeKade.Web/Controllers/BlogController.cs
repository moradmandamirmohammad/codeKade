using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.DTOs.Blog;
using Microsoft.AspNetCore.Mvc;

namespace codeKade.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Index(FilterBlogDTO filter)
        {
            var data = await _blogService.GetAll(filter);
            return View(data);
        }

        public async Task<IActionResult> Details(long id)
        {
            await _blogService.AddSeenToBlog(id);
            ViewBag.MostSeenBlog = await _blogService.GetMostSeenBlog();
            ViewBag.Categories = await _blogService.GetCategories();
            var data = await _blogService.GetBlogDetail(id);
            return View(data);
        }
    }
}
