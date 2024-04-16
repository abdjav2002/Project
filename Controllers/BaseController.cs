using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.Entity;
using System.Security.Claims;

namespace AspnetCoreMvcFull.Controllers
{
  public class BaseController : Controller
  {
    protected readonly DebtsyncContext _db;

    public BaseController(DebtsyncContext obj)
    {
      _db = obj;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
      var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
      List<Remark> list=[];
      var claimsIdentity2 = User.Identity as ClaimsIdentity;
      var roleIdClaim = claimsIdentity2?.FindFirst("roleID");
      if (roleIdClaim != null && Convert.ToInt32(roleIdClaim?.Value) == 5) { 
      var sidClaim = User.FindFirst(ClaimTypes.Sid);
      if (sidClaim != null && int.TryParse(sidClaim.Value, out int userId))
      {
        // Fetch user details
        var x = _db.Campaignslists.Where(x => x.UserIdFk == userId).ToList();

        foreach (var y in x)
        {
          var z = _db.Campaignslistusers.Where(x => x.CampaignslistsIdFk == y.CampaignslistsId).ToList();

            foreach (var a in z)
            {
              var acc = _db.Accounts.OrderByDescending(x=>x.CreatedAt).FirstOrDefault(x=>x.CustomerIdFk==a.UserIdFk);
              if(acc != null)
              {
                var r = _db.Remarks.OrderByDescending(x => x.CreatedAt).Where(x => x.AccountIdFk == acc.Accountid && x.PtpDate == currentDate).ToList();
                //var r = _db.Remarks.Include(x => x.StatuscodeIdFkNavigation).OrderByDescending(x => x.CreatedAt).Where(x => x.AccountIdFk == acc.Accountid && x.PtpDate == currentDate).ToList();
                if (r != null)
                {
                  foreach (var item in r)
                  {
                    list.Add(item);
                    break;
                  }
                }
              }
            }
          }

          // Pass user details to ViewData or ViewBag
          ViewBag.notificationList = list ;
          //ViewBag.detail = count ;
        //ViewData["UserDetails"] = x;
        // or ViewBag.UserDetails = userDetails;
      }
    }
      base.OnActionExecuting(context);
    }
  }
}
