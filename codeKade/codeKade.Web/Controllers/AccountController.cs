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
}