using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.DTOs.Blog;
using codeKade.Web.HttpContext;
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

        public async Task<IActionResult> Add()
        {
            ViewBag.Categories = await _blogService.GetCategories();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogDTO add)
        {
            add.UserId = User.GetUserID();
            add.ImageName = add.Image.FileName;
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _blogService.GetCategories();
                return View();
            }

            if (await _blogService.AddBlog(add))
            {

                TempData[SuccessMessage] = "مقاله با موفقیت افزوده بود";
                return RedirectToAction("Index","Blog" ,new {Area="Admin"});
            }

            return View();
        }
    }
}
