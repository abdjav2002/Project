using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AspnetCoreMvcFull.Controllers;

public class UsersController : BaseController
{
  public UsersController(DebtsyncContext obj) : base(obj)
  {
  }

  [HttpGet]
  public IActionResult UsersByRole(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    var users = _db.Users.Where(a => a.RoleIdFk == id).ToList();
    return Json(users); // Return the data as JSON
  }


  public IActionResult List() {
    var t = _db.Users.Where(a => a.RoleIdFk != 2).Count();
    var a = _db.Users.Where(a=>a.Status==1 && a.RoleIdFk != 2).Count();
    var na = _db.Users.Where(a=>a.Status==0 && a.RoleIdFk != 2).Count();
    var x = _db.Roles.Where(a => a.RoleId != 2).ToList();
    ViewBag.TotalUsers = t;
    ViewBag.ActiveUsers = a;
    ViewBag.NotActiveUsers = na;
    ViewBag.listRoles = x;
    ViewBag.UserDetail = _db.Users.Include(x=>x.RoleIdFkNavigation).Include(c=>c.CreatedbyIdFkNavigation).Include(f => f.CreatedtoIdFkNavigation).Where(a=>a.RoleIdFk!=2).ToList();
    return View();
  }
  public async Task<IActionResult> changeStatusUser(int id) {
    var u = await _db.Users.FirstOrDefaultAsync(a => a.UserId == id);
    if (u == null)
    {
      return NotFound($"User with UserIdFk {id} not found.");
    }

    // Update the status
    u.Status = u.Status==1?0:1;
    u.UpdatedAt = DateTime.UtcNow;

    _db.Update(u);
    await _db.SaveChangesAsync();
    return RedirectToAction(nameof(List));
  }

  [HttpPost]
  public IActionResult Create(User user)
  {
    var sidClaim = User.FindFirst(ClaimTypes.Sid)?.Value; // Access the value of the claim

    if (!string.IsNullOrEmpty(sidClaim))
    {
      if (ModelState.IsValid)
      {
        // Set CreatedAt and UpdatedAt properties
        if (user.CreatedtoIdFk == null)
        {
          user.CreatedtoIdFk = Convert.ToInt32(sidClaim);
        }
        user.CreatedbyIdFk = Convert.ToInt32(sidClaim);
        user.Status = 1;
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        // You may want to hash the password before storing it
        // user.Password = HashPassword(user.Password);

        _db.Users.Add(user);
        _db.SaveChanges();

        return RedirectToAction(nameof(List)); // Redirect to the list page after successful creation
      }
    }

    return View(user); // If model state is not valid or SID is not found, return the view with validation errors
  }


  public IActionResult ViewAccount() => View();
  public IActionResult ViewBilling() => View();
  public IActionResult ViewConnections() => View();
  public IActionResult ViewNotifications() => View();
  public IActionResult ViewSecurity() => View();
}
