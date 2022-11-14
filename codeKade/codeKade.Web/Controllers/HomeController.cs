using codeKade.Application.Services.Interfaces;
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
    }
}