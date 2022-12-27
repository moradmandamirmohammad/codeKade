using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace codeKade.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Authorize]
    public class AdminBaseController : Controller
    {
    }
}
