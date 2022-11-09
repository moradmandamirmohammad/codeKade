using System.Security.Claims;
using codeKade.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using codeKade.Web.Controllers;
using codeKade.DataLayer.DTOs.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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

    #region Regsiter

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDTO register)
    {
        if (!ModelState.IsValid)
        {
            return View(register);
        }

        if (register.Password.Length < 8)
        {
            ModelState.AddModelError("Password","پسورد باید بیشتر از 8 کارکتر باشد");
            return View(register);
        }

        var res = await _userService.Register(register);
        switch (res)
        {
            case RegisterUserResult.EmailConflict:
                ModelState.AddModelError("Eamil", "این ایمیل توسط شخص دیگری استفاده شده است");
                return View(register);
                break;
            case RegisterUserResult.MobileConflict:
                ModelState.AddModelError("Mobile", "این موبایل توسط شخص دیگری استفاده شده است");
                return View(register);
                break;
            case RegisterUserResult.Success:
                return Redirect("/");
                //Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi("486448347338723668753172656168572F5876356D787A55414B3343557A7752734350477430583669436B3D");
                //var result = api.Send("10008663", "09376443976", $"سلام {user.UserName} عزیز برای فعالسازی حساب کاربری خود در تاپ لرن روی لینک زیر کلیک کن  https://localhost:44341/ActiveAccount/{user.ActiveCode}");
                break;



        }

        return Redirect("/");
    }


    #endregion

    #region Login

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDTO login)
    {
        if (!ModelState.IsValid)
        {
            return View(login);
        }

        var res = await _userService.LoginUser(login);

        switch (res)
        {
            case LoginUserResult.NotActive:
                ModelState.AddModelError("Email", "ایمیل وارد شده هنور فعال نشده است");
                return View(login);
                break;
            case LoginUserResult.NotFound:
                ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده پیدا نشد");
                return View(login);
                break;
            case LoginUserResult.Success:
                var user = await _userService.GetEntityByEmail(login.Email);
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.NameIdentifier,user.ID.ToString()),
                    new Claim(ClaimTypes.Name,user.FirstName)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var prencipal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = login.RememberMe,
                };
                await HttpContext.SignInAsync(prencipal, properties);
                //if (returnUrl == null)
                //{
                //    return RedirectToAction("Index", "Home");
                //}
                //return Redirect(returnUrl);
            break;
        }

        return Redirect("/");
    }

    #endregion
}