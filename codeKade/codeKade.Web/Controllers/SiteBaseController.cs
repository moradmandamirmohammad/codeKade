using Microsoft.AspNetCore.Mvc;

namespace codeKade.Web.Controllers
{
    public class SiteBaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string InfoMessage = "InfoMessage";
        protected string WarningMessage = "WarningMessage";

        protected string BaseURL = "https://localhost:44358/";
    }
}
