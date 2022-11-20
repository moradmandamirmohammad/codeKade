using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using codeKade.Application.Senders;
using codeKade.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using codeKade.DataLayer.DTOs.Account;
using codeKade.Web.Convertors;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace codeKade.Web.Controllers;

public class AccountController : SiteBaseController
{
    #region constractor

    private readonly IUserService _userService;
    private readonly IViewRenderService _viewRenderService;


    public AccountController(IUserService userService, IViewRenderService viewRenderService)
    {
        _userService = userService;
        _viewRenderService = viewRenderService;
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
                TempData[ErrorMessage] = "ایمیل وارد شده تکراری میباشد";
                return View(register);
                break;
            case RegisterUserResult.MobileConflict:
                TempData[ErrorMessage] = "شماره موبایل وارد شده تکراری میباشد";
                return View(register);
                break;
            case RegisterUserResult.Success:
                //var email = register.Email;
                //var user = await _userService.GetEntityByEmail(email);
                //var body = _viewRenderService.RenderToStringAsync("Emails/ActiveEmail", user);
                //EmailSender.SendEmail(register.Email, "فعالسازی حساب کاربری", body);
                var user = await _userService.GetEntityByEmail(register.Email);
                var body = "برای فعالسازی وارد لینک روبرو شوید : " + BaseURL + "ActiveAccount/" + user.ActiveCode;
                sendMail(register.Email, body);
                TempData[SuccessMessage] = "حساب شما با موفقیت ایجاد شد برای فعالسازی حساب ایمیل خود را چک کنید";
                return Redirect("/");
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

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        TempData[SuccessMessage] = "شما با موفقیت خارج شدید";
        return Redirect("/");
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
                    new Claim(ClaimTypes.Name,user.FirstName + " " + user.LastName),
                    new Claim("Avatar",user.Avatar),
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var prencipal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = login.RememberMe,
                };
                await HttpContext.SignInAsync(prencipal, properties);
                TempData[SuccessMessage] = user.FirstName + " " + "عزیز خوش آمدید";
                break;
        }

        return Redirect("/");
    }

    #endregion

    #region Email

    public bool sendMail(string to, string code)
    {
        var client = new SmtpClient("smtp.mailtrap.io", 2525)
        {
            Credentials = new NetworkCredential("8e80c2d815c40d", "93e83c9989854e"),
            EnableSsl = true
        };
        client.Send("from@example.com", to, "ActiveCode", code);
        return true;
    }

    #endregion

    #region Active Account
    [HttpGet("ActiveAccount/{activeCode}")]
    public async Task<IActionResult> ActiveAccount(string activeCode)
    {
        if (!string.IsNullOrEmpty(activeCode))
        {
            var res = await _userService.ActiveAccount(activeCode);
            if (res == true)
            {
                TempData[SuccessMessage] = "حساب کاربری شما با موفقیت فعال شد";
                return Redirect("Login");
            }
        }
        return RedirectToAction("Index", "Home");
    }
    #endregion

    #region Forgot Password

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgot)
    {
        var user = await _userService.GetEntityByEmail(forgot.EmailAddress);
        if (user != null)
        {
            var body = "برای بازیابی کلمه عبور وارد لینک روبرو شوید : " + BaseURL +"ResetPassword/" + user.ActiveCode;
            sendMail(user.Email, body);
            TempData[SuccessMessage] = "ایمیل بازیابی برای شما ارسال شد";
            return Redirect("/");
        }

        TempData[ErrorMessage] = "کاربری با مشخصات وارد شده پیدا نشد";
        return View(forgot);
    }

    #endregion

    #region Reset Password

    [HttpGet("ResetPassword/{activeCode}")]
    public async Task<IActionResult> ResetPassword(string activeCode)
    {
        if (activeCode == null)
        {
            TempData[ErrorMessage] = "متاسفانه مشکلی پیش آمد";
            return Redirect("/");
        } 
        var user = await _userService.GetUserByActiveCode(activeCode);
        if (user == null)
        {
            TempData[ErrorMessage] = "متاسفانه مشکلی پیش آمد";
            return Redirect("/");
        }
        return View();
    }

    [HttpPost("ResetPassword/{activeCode}")]
    public async Task<IActionResult> ResetPassword(string activeCode,ResetPasswordDTO reset)
    {
        if (ModelState.IsValid)
        {
            if (activeCode != null)
            {
                var res = await _userService.ResetPassword(activeCode, reset);
                if (res)
                {
                    TempData[SuccessMessage] = "کلمه عبور شما با موفقیت تغییر یافت";
                    return Redirect("/");
                }
            }
        }
        return View();
    }

    #endregion
}