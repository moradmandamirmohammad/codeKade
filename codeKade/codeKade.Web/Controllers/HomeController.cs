using codeKade.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codeKade.Web.Controllers
{
    public class HomeController : SiteBaseController
    {
        private readonly IEventService _eventService;

        public HomeController(IEventService eventService)
        {
            _eventService = eventService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.ActiveEvents = await _eventService.GetNewEvents();
            return View();
        }

        public async Task<IActionResult> EventDetail(long id)
        {
            var single_event = await _eventService.GetSingleEvent(id);
            return View(single_event);
        }
    }
}