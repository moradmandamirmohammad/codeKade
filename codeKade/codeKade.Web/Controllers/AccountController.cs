using codeKade.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using codeKade.Web.Controllers;
using codeKade.DataLayer.DTOs.Account;

namespace codeKade.Web.Controllers;

public class AccountController : BaseController
{
    #region constractor

    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    #endregion

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }
}