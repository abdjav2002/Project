using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Controllers;

public class AccessController : Controller
{
  private readonly DebtsyncContext _db;

  public AccessController(DebtsyncContext db)
  {
    _db = db;
  }
  public IActionResult Permission() => View();
  public IActionResult Roles() {
    ViewBag.listUsers = _db.Users.Where(a=>a.Status==1).ToList();
    return View(_db.Roles.Where(a=>a.RoleId!=2).OrderBy(a => a.RoleId).ToList());
  }
}
