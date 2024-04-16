using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AspnetCoreMvcFull.Controllers;

public class AuthController : Controller
{
  protected readonly DebtsyncContext _db;

  public AuthController(DebtsyncContext obj)
  {
    _db = obj;
  }

  public IActionResult ForgotPasswordBasic() => View();
  public IActionResult ForgotPasswordCover() => View();

  public IActionResult Logout()
  {
    //HttpContext.Session.Clear();
    var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return RedirectToAction(nameof(Login));
  }

  public IActionResult Login() {

    return View();
  }

  [AllowAnonymous]
  [HttpPost]
  public IActionResult Login([Bind("UserName,Password")] User U, string ReturnUrl)
  {
    ViewBag.ReturnUrl = ReturnUrl;
    var user = _db.Users
    .Where(x => x.UserName == U.UserName && x.Password == U.Password)
    .Join(_db.Roles, user => user.RoleIdFk, role => role.RoleId, (user, role) => new {
      User = user,
      Role = role
    })
    .FirstOrDefault();

    if (user != null)
    {
      var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, user.User.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.User.UserName),
            new Claim("roleID", user.User.RoleIdFk.ToString()),
            new Claim(ClaimTypes.Role, user.Role.RoleName),
        };

      var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
      var principal = new ClaimsPrincipal(identity);
      var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

      if (string.IsNullOrEmpty(ReturnUrl))
      {
        if(user.User.RoleIdFk == 1)
        {
        return RedirectToAction("Index", "Dashboards");
        }
        else if (user.User.RoleIdFk == 3)
        {
          return RedirectToAction("Index", "Managers");
        }
        else if (user.User.RoleIdFk == 4)
        {
          return RedirectToAction("Index", "Supervisor");
        }
        else if (user.User.RoleIdFk == 5)
        {
          return RedirectToAction("Officer", "Dashboards");
        }
        else if (user.User.RoleIdFk == 6)
        {
          return RedirectToAction("Index", "InboundOfficer");
        }
        else if (user.User.RoleIdFk == 7)
        {
          return RedirectToAction("Index", "QA");
        }
        else if (user.User.RoleIdFk == 8)
        {
          return RedirectToAction("Index", "FieldOfficer");
        }
      }
      else
      {
        return Redirect(ReturnUrl);
      }
    }
    ViewBag.chkLog = "INVALID CREDENTIALS";
    return View(U);
  }

  public IActionResult LoginCover() => View();
  public IActionResult RegisterBasic() => View();
  public IActionResult RegisterCover() => View();
  public IActionResult RegisterMultiSteps() => View();
  public IActionResult ResetPasswordBasic() => View();
  public IActionResult ResetPasswordCover() => View();
  public IActionResult TwoStepsBasic() => View();
  public IActionResult TwoStepsCover() => View();
  public IActionResult VerifyEmailBasic() => View();
  public IActionResult VerifyEmailCover() => View();
}
