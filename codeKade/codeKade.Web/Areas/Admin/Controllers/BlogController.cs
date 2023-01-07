using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.DTOs.Blog;
using Microsoft.AspNetCore.Mvc;

namespace codeKade.Web.Areas.Admin.Controllers
{
    public class BlogController : AdminBaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IActionResult> Index(FilterBlogDTO filter)
        {
            var blogs = await _blogService.GetAll(filter);
            return View(blogs);
        }

        public async Task<IActionResult> DeletedBlogs(FilterBlogDTO filter)
        {
            return View(await _blogService.GetDeletedBlogs(filter));
        }
    }
}
