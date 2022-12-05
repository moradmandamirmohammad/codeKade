using codeKade.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace codeKade.Web.Controllers
{
    public class EventController : Controller
    {

        private readonly IEventService _eventService;
        private readonly ICommentService _commentService;

        public EventController(IEventService eventService, ICommentService commentService)
        {
            _eventService = eventService;
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(long id)
        {
            var single_event = await _eventService.GetSingleEvent(id);
            ViewBag.Comments = await _commentService.GetEventComments(id);
            return View(single_event);
        }
    }
}
