using codeKade.Application.Services.Interfaces;
using codeKade.DataLayer.DTOs.Comment;
using codeKade.DataLayer.Migrations;
using codeKade.Web.HttpContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codeKade.Web.Controllers
{
    public class HomeController : SiteBaseController
    {
        private readonly IEventService _eventService;
        private readonly ICommentService _commentService;

        public HomeController(IEventService eventService, ICommentService commentService)
        {
            _eventService = eventService;
            _commentService = commentService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.ActiveEvents = await _eventService.GetNewEvents();
            return View();
        }

        public async Task<IActionResult> EventDetail(long id)
        {
            var single_event = await _eventService.GetSingleEvent(id);
            ViewBag.Comments = await _commentService.GetEventComments(id);
            return View(single_event);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(AddcommentDTO comments)
        {
            await _commentService.AddComment(comments.Text, User.GetUserID(), comments.EventId, comments.ParentId);
            TempData[SuccessMessage] = "نظر شما با موفقیت ثبت شد";
            return Redirect("/Home/EventDetail/"+ comments.EventId);
        }
    }
}