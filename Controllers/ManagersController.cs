using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AspnetCoreMvcFull.Controllers
{
  public class ManagersController : Controller
  {

    private readonly DebtsyncContext _db;

    public ManagersController(DebtsyncContext db)
    {
      _db = db;
    }
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Users()
    {
      var sidClaim = User.FindFirst(ClaimTypes.Sid)?.Value; // Access the value of the claim

      if (!string.IsNullOrEmpty(sidClaim))
      {
        var t = _db.Users.Where(a => a.CreatedtoIdFk == Convert.ToInt32(sidClaim)).Count();
        var a = _db.Users.Where(a => a.Status == 1 && a.CreatedtoIdFk == Convert.ToInt32(sidClaim)).Count();
        var na = _db.Users.Where(a => a.Status == 0 && a.CreatedtoIdFk == Convert.ToInt32(sidClaim)).Count();
        var x = _db.Roles.ToList();
        ViewBag.TotalUsers = t;
        ViewBag.ActiveUsers = a;
        ViewBag.NotActiveUsers = na;
        ViewBag.listRoles = x;
        ViewBag.UserDetail = _db.Users.Include(x => x.RoleIdFkNavigation).Where(a => a.RoleIdFk != 2 && a.CreatedtoIdFk == Convert.ToInt32(sidClaim)).ToList();

      }
      return View();
    }
  }
}
