using Microsoft.AspNetCore.Mvc;
using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Authorization;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using NuGet.Packaging.Signing;

namespace AspnetCoreMvcFull.Controllers;


public class DashboardsController : BaseController
{
  

  public DashboardsController(DebtsyncContext obj) : base(obj)
  {
    
  }

  // Method for inserting a new Note record
  public async Task<IActionResult> addNote(int id,String? EmiratesId,
    String? SocOrRecWeb, String? CompanySearch, String? ImmigrationChk,
    String? Feedback, String? NewContactOrAddress, String? SpecialInfo, String? EmailSearchOrBobox)
  {
      try
      {
      var obj = new Note
      {
        EmiratesId = EmiratesId,
        CompanySearch = CompanySearch,
        EmailSearchOrBobox = EmailSearchOrBobox,
        Feedback = Feedback,
        ImmigrationChk = ImmigrationChk,
        NewContactOrAddress = NewContactOrAddress,
        SocOrRecWeb = SocOrRecWeb,
        SpecialInfo = SpecialInfo,
        Status = 1,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
        AccountIdFk = id,
      };
      // Add the new Note record to the DbContext
      _db.Add(obj);

        // Save changes to the database
        await _db.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        // Handle any exceptions
        Console.WriteLine($"An error occurred while inserting the Note record: {ex.Message}");
    }

    int[] x = { id };

    return RedirectToAction(nameof(updateRemarks), new { id = x });
  }

  public async Task<IActionResult> summary(int? id)
  {
    var list = await _db.Campaignslists.FirstOrDefaultAsync(a => a.CampaignslistsId == id);
    var z = await _db.Campaignslistusers.Where(a => a.CampaignslistsIdFk == id).ToListAsync();
    var y = await _db.Accounts.ToListAsync();
    // Filter accounts using client-side evaluation
    var filteredAccounts = y.Where(a => z.Any(c => c.UserIdFk == a.CustomerIdFk)).ToList();

    decimal? Outstandingbalance = filteredAccounts.Sum(a => a.Outstandingbalance);

    var t = _db.Remarks
    .Include(a => a.AccountIdFkNavigation)
    .Include(a => a.ActioncodeIdFkNavigation)
    .Include(a => a.StatuscodeIdFkNavigation)
    .Include(a => a.UserIdFkNavigation)
    .AsEnumerable() // Bring data into memory
    .Where(a => filteredAccounts.Any(c => c.Accountid == a.AccountIdFk))
    .ToList(); // Perform client-side filtering

    DateTime localDateTime = DateTime.UtcNow;
    var t_25 = 0;
    var t_15 = 0;
    var t_10 = 0;
    var t_5 = 0;
    var t_3 = 0;
    decimal? a_25 = 0;
    decimal? a_15 = 0;
    decimal? a_10 = 0;
    decimal? a_5 = 0;
    decimal? a_3 = 0;
    var cont = 0;
    var nonCont = 0;
    var skip = 0;
    var wrong = 0;
    var ptp = 0;
    var bp = 0;
    decimal? a_cont = 0;
    decimal? a_nonCont = 0;
    decimal? a_skip = 0;
    decimal? a_wrong = 0;
    decimal? a_ptp = 0;
    decimal? a_bp = 0;

    foreach (var r in t)
    {
      if (r.StatuscodeIdFk == 1)
      {
        bp++;
        a_bp += r.AccountIdFkNavigation!.Outstandingbalance;
      }
      else if (r.StatuscodeIdFk == 4)
      {
        ptp++;
        a_ptp += r.AccountIdFkNavigation!.Outstandingbalance;
      }
      else if (r.StatuscodeIdFk == 7)
      {
        cont++;
        a_cont += r.AccountIdFkNavigation!.Outstandingbalance;
      }
      else if (r.StatuscodeIdFk == 8)
      {
        nonCont++;
        a_nonCont += r.AccountIdFkNavigation!.Outstandingbalance;
      }
      else if (r.StatuscodeIdFk == 9)
      {
        skip++;
        a_skip += r.AccountIdFkNavigation!.Outstandingbalance;
      }
      else if (r.StatuscodeIdFk == 10)
      {
        wrong++;
        a_wrong += r.AccountIdFkNavigation!.Outstandingbalance;
      }

      // Calculate the difference in days between the current date and the UpdatedAt date
      TimeSpan difference = localDateTime - r.UpdatedAt.Value;
      int daysDifference = (int)difference.TotalDays;

      // Check the difference and increment the respective counters
      if (daysDifference > 25)
      {
        t_25++;
        a_25 += r.AccountIdFkNavigation!.Outstandingbalance;
      }
      else if (daysDifference > 15)
      {
        t_15++;
        a_15 += r.AccountIdFkNavigation!.Outstandingbalance;
      }
      else if (daysDifference > 10)
      {
        t_10++;
        a_10 += r.AccountIdFkNavigation!.Outstandingbalance;
      }
      else if (daysDifference > 5)
      {
        t_5++;
        a_5 += r.AccountIdFkNavigation!.Outstandingbalance;
      }
      else if (daysDifference > 3)
      {
        t_3++;
        a_3 += r.AccountIdFkNavigation!.Outstandingbalance;
      }
    }

    ViewBag.os = Outstandingbalance;
    ViewBag.list = list;
    ViewBag.t_25 = t_25;
    ViewBag.t_15 = t_15;
    ViewBag.t_10 = t_10;
    ViewBag.t_5 = t_5;
    ViewBag.t_3 = t_3;
    ViewBag.a_25 = a_25;
    ViewBag.a_15 = a_15;
    ViewBag.a_10 = a_10;
    ViewBag.a_5 = a_5;
    ViewBag.a_3 = a_3;
    ViewBag.cont = cont;
    ViewBag.nonCont = nonCont;
    ViewBag.skip = skip;
    ViewBag.wrong = wrong;
    ViewBag.bp = bp;
    ViewBag.ptp = ptp;
    ViewBag.a_cont = a_cont;
    ViewBag.a_nonCont = a_nonCont;
    ViewBag.a_skip = a_skip;
    ViewBag.a_wrong = a_wrong;
    ViewBag.a_bp = a_bp;
    ViewBag.a_ptp = a_ptp;
    return View(filteredAccounts);
  }
  public async Task<IActionResult> custDet(int ?id)
  {
    if (id == null)
    {
      return NotFound();
    }
    var y = _db.Accounts.Include(a => a.CustomerIdFkNavigation).Include(a => a.EmailIdFkNavigation).Where(a=>a.Accountid == id).First();
    var x = _db.Accounts
      .Include(a=>a.CenterIdFkNavigation)
      .Include(a=>a.CountryIdFkNavigation)
      .Include(a=>a.CustomerIdFkNavigation)
      .Include(a=>a.EmailIdFkNavigation)
      .Include(a=>a.OrganizationIdFkNavigation)
      .Include(a=>a.ProductIdFkNavigation)
      .Include(a=>a.RegionIdFkNavigation)
      .Include(a=>a.RemarksNavigation)
      .Include(a=>a.SegmentIdFkNavigation)
      .Include(a=>a.UserIdFkNavigation)
      .OrderByDescending(a=>a.CreatedAt)
      .Where(a => a.CustomerIdFk == y.CustomerIdFk).ToList();
    ViewBag.user = y;

    var assigned = _db.Campaignslistusers
      .Include(a=>a.CampaignslistsIdFkNavigation)
      .ThenInclude(a=>a.CampaignIdFkNavigation)
      .Include(a=>a.CampaignslistsIdFkNavigation)
      .ThenInclude(a=>a.UserIdFkNavigation)
      .Where(a => a.UserIdFk == y.CustomerIdFk && a.Status!=-1).ToList();
    ViewBag.assigned = assigned;
    return View(x);
  }
    public async Task<IActionResult> FollowUps()
  {
    var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
    List<Remark> list = [];
    var claimsIdentity2 = User.Identity as ClaimsIdentity;
    var roleIdClaim = claimsIdentity2?.FindFirst("roleID");
    if (roleIdClaim != null && Convert.ToInt32(roleIdClaim?.Value) == 5)
    {
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
            var acc = _db.Accounts.OrderByDescending(x => x.CreatedAt).FirstOrDefault(x => x.CustomerIdFk == a.UserIdFk);
            if (acc != null)
            {
              var r = _db.Remarks.Include(q=>q.StatuscodeIdFkNavigation).Include(q => q.ActioncodeIdFkNavigation).OrderByDescending(x => x.CreatedAt).Where(x => x.AccountIdFk == acc.Accountid && x.PtpDate == currentDate).ToList();
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
      }
    }
    return View(list);
  }

  

  // GET: Campaignslist/EditTarget/5
  public async Task<IActionResult> Target(int? id)
  {

    if (id == null)
    {
      return NotFound();
    }

    var campaignslist = await _db.Campaignslists.FindAsync(id);
    if (campaignslist == null)
    {
      return NotFound();
    }

    var x = await _db.Campaignslistusers.Where(a => a.CampaignslistsIdFk == id).ToListAsync();
    decimal? total = 0;
    foreach(var i in x)
    {
      var y = await _db.Accounts.OrderByDescending(a => a.CreatedAt).FirstOrDefaultAsync(q=>q.CustomerIdFk==i.UserIdFk);
      total += y!.Overdueamount;
    }

    ViewBag.total = total;
    ViewBag.id = id;
    return View(campaignslist);
  }

  // POST: Campaignslist/EditTarget/5
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> Target([Bind("CampaignslistsId,Target")] Campaignslist campaignslist)
  {

    if (ModelState.IsValid)
    {
      try
      {
        var campaignslistExisting = await _db.Campaignslists.FindAsync(campaignslist.CampaignslistsId);
        if (campaignslistExisting == null)
        {
          return NotFound();
        }
        campaignslistExisting.Target = campaignslist.Target;
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CampaignslistExists(campaignslist.CampaignslistsId))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }
      return RedirectToAction("Detail", "Campaigns");
    }
    ViewBag.id = campaignslist.CampaignslistsId;
    return View(campaignslist);
  }

  private bool CampaignslistExists(int id)
  {
    return _db.Campaignslists.Any(e => e.CampaignslistsId == id);
  }

  public IActionResult OfficerListsDetail(int id, string fcon = "All", string fseg = "All", string freg = "All", string forg = "All", string fcen = "All", string fpro = "All")
  {
    ViewBag.listId = id;
    IQueryable<Account> query = _db.Accounts
        .Include(a => a.CountryIdFkNavigation)
        .Include(a => a.EmailIdFkNavigation)
        .Include(a => a.OrganizationIdFkNavigation)
        .Include(a => a.RegionIdFkNavigation)
        .Include(a => a.SegmentIdFkNavigation)
        .Include(a => a.ProductIdFkNavigation)
        .Include(a => a.CenterIdFkNavigation)
        .Include(a => a.CustomerIdFkNavigation)
        .Include(a => a.UserIdFkNavigation);

    if (fcon != "All")
    {
      int a = Convert.ToInt32(fcon);
      query = query.Where(u => u.CountryIdFk == a);
    }

    if (fseg != "All")
    {
      int a = Convert.ToInt32(fseg);
      query = query.Where(u => u.SegmentIdFk == a);
    }

    if (freg != "All")
    {
      int a = Convert.ToInt32(freg);
      query = query.Where(u => u.RegionIdFk == a);
    }

    if (forg != "All")
    {
      int a = Convert.ToInt32(forg);
      query = query.Where(u => u.OrganizationIdFk == a);
    }

    if (fcen != "All")
    {
      int a = Convert.ToInt32(fcen);
      query = query.Where(u => u.CenterIdFk == a);
    }

    if (fpro != "All")
    {
      int a = Convert.ToInt32(fpro);
      query = query.Where(u => u.ProductIdFk == a);
    }

    // Modify query to include only accounts that are present in BlockAccounts
    query = query.Join(_db.Campaignslistusers.Where(ba => ba.CampaignslistsIdFk == id),
                       account => account.CustomerIdFk,
                       c => c.UserIdFk,
                       (account, c) => account);

    var result = query.ToList();

    // Materialize the query into a list

    ViewBag.vcon = fcon ?? "";
    ViewBag.vreg = freg ?? "";
    ViewBag.vseg = fseg ?? "";
    ViewBag.vorg = forg ?? "";
    ViewBag.vcen = fcen ?? "";
    ViewBag.vpro = fpro ?? "";

    ViewBag.Nation = _db.Nationalities.ToList();
    ViewBag.country = _db.Countries.ToList();
    ViewBag.segment = _db.Segments.ToList();
    ViewBag.region = _db.Regions.ToList();
    ViewBag.organization = _db.Organizations.ToList();
    ViewBag.product = _db.Products.ToList();
    ViewBag.center = _db.Centers.ToList();

    return View(result);
  }


  public IActionResult updateRemarks(int[] id)
  {
    if (id.Length <= 0)
    {
      return RedirectToAction(nameof(OfficerLists));
    }
    ViewBag.accId = id;
    ViewBag.remarks = _db.Remarks
      .Include(b=>b.StatuscodeIdFkNavigation)
      .Include(b => b.ActioncodeIdFkNavigation)
      .Include(b => b.UserIdFkNavigation)
      .Where(a => a.AccountIdFk == Convert.ToInt32(id[0])).OrderByDescending(x=>x.CreatedAt).ToList();
    var n = _db.Notes.OrderByDescending(a => a.CreatedAt).Where(a => a.AccountIdFk == id[0]).ToList();
    if (n != null && n.Count() > 0)
    {
      ViewBag.notes = n[0];
    }
    ViewBag.statusCode = _db.Statuscodes.ToList();
    ViewBag.actionCode = _db.Actioncodes.ToList();
    ViewBag.accIdSingle = id[0];
    return View(_db.Accounts.Include(x=>x.CustomerIdFkNavigation)
      .Include(x => x.ProductIdFkNavigation)
      .Include(x => x.OrganizationIdFkNavigation)
      .Include(x => x.SegmentIdFkNavigation)
      .FirstOrDefault(a => a.Accountid == Convert.ToInt32(id[0])));
  }

  [HttpPost]
  public IActionResult UpdateRemarks(int accountIdd,int StatuscodeId,String Remarkss, int ActioncodeId, String PtpAmount,String PtpDate,String[] idd)
  {
    //int[] ids = idd.Split(',').Select(int.Parse).ToArray();
    int[] ids = idd[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

    var sidClaim = User.FindFirst(ClaimTypes.Sid);
    if (sidClaim != null && int.TryParse(sidClaim.Value, out int userId))
    {
      // Insert a new remark record
      var newRemark = new Remark
      {
        AccountIdFk = accountIdd, // Assuming there's an AccountId property in the Remark model
        RemarkText = Remarkss,
        PtpAmount = Convert.ToInt32(PtpAmount),
        PtpDate = PtpDate,
        StatuscodeIdFk = StatuscodeId,
        ActioncodeIdFk = ActioncodeId,
        UserIdFk = userId,
        CreatedAt = DateTime.UtcNow, // You might need to adjust this according to your requirements
        UpdatedAt = DateTime.UtcNow // You might need to adjust this according to your requirements
      };

      _db.Remarks.Add(newRemark);
      _db.SaveChanges();

      return RedirectToAction(nameof(updateRemarks), new { id = ids });
    }
    ViewBag.remarks = _db.Remarks
      .Include(b => b.StatuscodeIdFkNavigation)
      .Include(b => b.ActioncodeIdFkNavigation)
      .Include(b => b.UserIdFkNavigation)
      .Where(a => a.AccountIdFk == ids[0]).ToList();
    ViewBag.statusCode = _db.Statuscodes.ToList();
    ViewBag.actionCode = _db.Actioncodes.ToList();
    ViewBag.accId = ids;
    ViewBag.accIdSingle = ids[0];
    return View();
  }


  public IActionResult StatusCode()
  {
    return View(_db.Statuscodes.ToList());
  }

  // GET: Status/Create
  public IActionResult StatusCodeCreate()
  {
    return View();
  }

  // POST: Status/Create
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> StatusCodeCreate([Bind("Statuscode1,Sname,Sdescription,FollowupDate,Amount,Color")] Statuscode sCode)
  {
    if (ModelState.IsValid)
    {
      sCode.UpdatedAt = DateTime.UtcNow;
      sCode.CreatedAt = DateTime.UtcNow;
      _db.Add(sCode);
      await _db.SaveChangesAsync();
      return RedirectToAction(nameof(StatusCode));
    }
    return View(sCode);
  }

  public IActionResult ActionCode()
  {
    return View(_db.Actioncodes.ToList());
  }

  // GET: Action/Create
  public IActionResult ActionCodeCreate()
  {
    return View();
  }

  // POST: Action/Create
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> ActionCodeCreate([Bind("Actioncode1,Aname,Adescription,FollowUpDate,Amount,Color")] Actioncode aCode)
  {
    if (ModelState.IsValid)
    {
      aCode.UpdatedAt = DateTime.UtcNow;
      aCode.CreatedAt = DateTime.UtcNow;
      _db.Add(aCode);
      await _db.SaveChangesAsync();
      return RedirectToAction(nameof(ActionCode));
    }
    return View(aCode);
  }


  public async Task<IActionResult> updateStatus(int? id, int status)
  {
    if (id == null)
    {
      return BadRequest("UserIdFk and newStatus must be provided.");
    }

      var account = await _db.Campaignslistusers.FirstOrDefaultAsync(a => a.UserIdFk == id);
      if (account == null)
      {
        return NotFound($"User with UserIdFk {id} not found.");
      }

      // Update the status
      account.Status = status;
      account.UpdatedAt = DateTime.UtcNow; // Update the UpdatedAt timestamp

      _db.Update(account);
      await _db.SaveChangesAsync();

      // Redirect to another action method
      return RedirectToAction(nameof(OfficerLists));
  }

  public IActionResult accounts(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }
    var z = _db.Accounts
        .Include(a => a.CountryIdFkNavigation)
        .Include(a => a.RegionIdFkNavigation)
        .Include(a => a.SegmentIdFkNavigation)
        .Include(a => a.OrganizationIdFkNavigation)
        .Include(a => a.EmailIdFkNavigation)
        .Include(a => a.CustomerIdFkNavigation)
        .Include(a => a.UserIdFkNavigation)
        .OrderByDescending(a => a.CreatedAt)
        .Where(x=>x.Accountid==id).ToList();
    return View(z);
  }


  public async Task<IActionResult> OfficerLists()
  {
    var sidClaim = User.FindFirst(ClaimTypes.Sid);
    if (sidClaim != null && int.TryParse(sidClaim.Value, out int userId))
    {
      var list = await _db.Campaigns
          .Where(x => x.Campaignslists.Any(b => b.UserIdFk == userId))
          .ToListAsync();

      ViewBag.list = await _db.Campaignslists
          .Include(a => a.UserIdFkNavigation)
          .Where(b => b.UserIdFk == userId)
          .ToListAsync();

      ViewBag.listUsers = await _db.Campaignslistusers
          .Include(a => a.UserIdFkNavigation)
          .Where(a => a.Status != -1 && a.Status != 0)
          .ToListAsync();

      ViewBag.accounts = await _db.Accounts
          .Include(a => a.CountryIdFkNavigation)
          .Include(a => a.RegionIdFkNavigation)
          .Include(a => a.SegmentIdFkNavigation)
          .Include(a => a.OrganizationIdFkNavigation)
          .Include(a => a.EmailIdFkNavigation)
          .Include(a => a.CustomerIdFkNavigation)
          .Include(a => a.UserIdFkNavigation)
          .OrderByDescending(a => a.CreatedAt)
          .ToListAsync();

      return View(list);
    }

    return View();
  }


  public IActionResult Officer()
  {
    var sidClaim = User.FindFirst(ClaimTypes.Sid);
    var lists = 0;
    var users = 0;
    var usersBound = 0;
    var usersInBound = 0;
    var uniqueListOfOrganizationname = new HashSet<string>();
    var uniqueListOfCountryname = new HashSet<string>();
    var uniqueListOfRegionname = new HashSet<string>();
    var uniqueListOfSegmentname = new HashSet<string>();
    var uniqueListOfProductname = new HashSet<string>();
    var uniqueListOfCentername = new HashSet<string>();

    if (sidClaim != null && int.TryParse(sidClaim.Value, out int userId))
    {
      var userLists = _db.Campaignslists
                          .Where(a => a.UserIdFk == userId).Include(z=>z.CampaignIdFkNavigation)
                          .ToList();

      foreach (var list in userLists)
      {
        lists++;
        var userListUsers = _db.Campaignslistusers
                                 .Where(a => a.CampaignslistsIdFk == list.CampaignslistsId)
                                 .ToList();

        // Now you have the current list (list) and its associated users (userListUsers)
        // You can process them as needed

        // For example, you can access the users like this:
        foreach (var user in userListUsers)
        {
          users++;
          var accounts = _db.Accounts
              .Include(u => u.OrganizationIdFkNavigation)
              .Include(u => u.CountryIdFkNavigation)
              .Include(u => u.RegionIdFkNavigation)
              .Include(u => u.SegmentIdFkNavigation)
              .Include(u => u.ProductIdFkNavigation)
              .Include(u => u.CenterIdFkNavigation)
              .Where(c => c.CustomerIdFk == user.UserIdFk).ToList();

          foreach (var acc in accounts)
          {
            if (!uniqueListOfOrganizationname.Contains(acc.OrganizationIdFkNavigation!.Organizationname!))
              uniqueListOfOrganizationname.Add(acc.OrganizationIdFkNavigation!.Organizationname!);

            if (!uniqueListOfCountryname.Contains(acc.CountryIdFkNavigation!.Countryname!))
              uniqueListOfCountryname.Add(acc.CountryIdFkNavigation!.Countryname!);

            if (!uniqueListOfRegionname.Contains(acc.RegionIdFkNavigation!.Regionname!))
              uniqueListOfRegionname.Add(acc.RegionIdFkNavigation!.Regionname!);

            if (!uniqueListOfSegmentname.Contains(acc.SegmentIdFkNavigation!.Segmentname!))
              uniqueListOfSegmentname.Add(acc.SegmentIdFkNavigation!.Segmentname!);

            if (!uniqueListOfProductname.Contains(acc.ProductIdFkNavigation!.Pname))
              uniqueListOfProductname.Add(acc.ProductIdFkNavigation!.Pname);

            if (!uniqueListOfCentername.Contains(acc.CenterIdFkNavigation!.Cname))
              uniqueListOfCentername.Add(acc.CenterIdFkNavigation!.Cname);
          }

          if (user.Status == 1)
          {
            usersBound++;
          }
          else
          {
            usersInBound++;
          }
        }
      }
    }

    ViewBag.lists = lists;
    ViewBag.users = users;
    ViewBag.usersBound = usersBound;
    ViewBag.usersInBound = usersInBound;
    ViewBag.uniqueListOfOrganizationname = uniqueListOfOrganizationname.ToList();
    ViewBag.uniqueListOfCountryname = uniqueListOfCountryname.ToList();
    ViewBag.uniqueListOfRegionname = uniqueListOfRegionname.ToList();
    ViewBag.uniqueListOfSegmentname = uniqueListOfSegmentname.ToList();
    ViewBag.uniqueListOfProductname = uniqueListOfProductname.ToList();
    ViewBag.uniqueListOfCentername = uniqueListOfCentername.ToList();

    return View();
  }



  public IActionResult Index() => View();

  [Authorize(Roles = "admin")]
  public IActionResult CRM() => View();

  public IActionResult history(string fcon = "All", string fseg = "All", string freg = "All", string forg = "All", string fcen = "All", string fpro = "All")
  {
    IQueryable<Account> query = _db.Accounts
        .Include(a => a.CountryIdFkNavigation)
        .Include(a => a.EmailIdFkNavigation)
        .Include(a => a.OrganizationIdFkNavigation)
        .Include(a => a.RegionIdFkNavigation)
        .Include(a => a.SegmentIdFkNavigation)
        .Include(a => a.ProductIdFkNavigation)
        .Include(a => a.CenterIdFkNavigation)
        .Include(a => a.CustomerIdFkNavigation)
        .Include(a => a.UserIdFkNavigation);

    if (fcon != "All")
    {
      int a = Convert.ToInt32(fcon);
      query = query.Where(u => u.CountryIdFk == a);
    }

    if (fseg != "All")
    {
      int a = Convert.ToInt32(fseg);
      query = query.Where(u => u.SegmentIdFk == a);
    }

    if (freg != "All")
    {
      int a = Convert.ToInt32(freg);
      query = query.Where(u => u.RegionIdFk == a);
    }

    if (forg != "All")
    {
      int a = Convert.ToInt32(forg);
      query = query.Where(u => u.OrganizationIdFk == a);
    }

    if (fcen != "All")
    {
      int a = Convert.ToInt32(fcen);
      query = query.Where(u => u.CenterIdFk == a);
    }

    if (fpro != "All")
    {
      int a = Convert.ToInt32(fpro);
      query = query.Where(u => u.ProductIdFk == a);
    }

    var result = query.ToList();

    // Materialize the query into a list

    ViewBag.vcon = fcon ?? "";
    ViewBag.vreg = freg ?? "";
    ViewBag.vseg = fseg ?? "";
    ViewBag.vorg = forg ?? "";
    ViewBag.vcen = fcen ?? "";
    ViewBag.vpro = fpro ?? "";

    ViewBag.Nation = _db.Nationalities.ToList();
    ViewBag.country = _db.Countries.ToList();
    ViewBag.segment = _db.Segments.ToList();
    ViewBag.region = _db.Regions.ToList();
    ViewBag.organization = _db.Organizations.ToList();
    ViewBag.product = _db.Products.ToList();
    ViewBag.center = _db.Centers.ToList();

    return View(result);
  }


  public IActionResult myExcel(List<Account>? uploaded)
  {
    return View(uploaded);
  }



  [HttpPost]
  public IActionResult myExcel(IFormFile file, int? id,String CampaignName, String CampaignDesc,String autoCempaign)
  {
    //if (id == null)
    //{
    //  return NotFound();
    //}
    if (file != null && file.Length > 0)
    {
      // Check file extension
      var allowedExtensions = new[] { ".xlsx", ".xml", ".json", ".doc" };
      var fileExtension = Path.GetExtension(file.FileName).ToLower();
      if (!allowedExtensions.Contains(fileExtension))
      {
        return Ok("Invalid file");
      }

      // Save the image to a folder
      string folderName = "ExcelUploads";

      string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

      if (!Directory.Exists(uploadsFolder))
      {
        Directory.CreateDirectory(uploadsFolder);
      }

      string uniqueFileName = Path.ChangeExtension(Path.GetRandomFileName(), fileExtension);
      string filePath = Path.Combine(uploadsFolder, uniqueFileName);
      using (var fileStream = new FileStream(filePath, FileMode.Create))
      {
        file.CopyTo(fileStream);
      }

      var excelDataList = ReadExcelData(filePath, CampaignName, CampaignDesc, autoCempaign);
      var counts = InsertDataToDatabase(excelDataList);


      ViewBag.TotalRows = counts.totalRows;
      ViewBag.InserterecdRows = counts.insertedRows;
      ViewBag.UpdatedRows = counts.updatedRows;

      System.IO.File.Delete(filePath);

      return View(excelDataList);
    }

    return RedirectToAction(nameof(myExcel));
  }
  private List<Account> ReadExcelData(string filePath,String CampaignName,String CampaignDesc,String autoCempaign)
  {
    var excelDataList = new List<Account>();

    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the license context

    using (var package = new ExcelPackage(new FileInfo(filePath)))
    {
      var sidClaim = User.FindFirst(ClaimTypes.Sid);

      if (sidClaim != null && int.TryParse(sidClaim.Value, out int userId))
      {

        var worksheet = package.Workbook.Worksheets[0];
      var rowCount = worksheet.Dimension.Rows;

        int id = GetOrCreateCampaignsId(CampaignName, CampaignDesc);
        int count = 1;

        for (int row = 2; row <= rowCount; row++)
      {
        String organization = worksheet.Cells[row, 3].Value?.ToString().ToLower();//C
          String country = worksheet.Cells[row, 4].Value?.ToString().ToLower();//D
        String region = worksheet.Cells[row, 5].Value?.ToString().ToLower();//E
        String center = worksheet.Cells[row, 6].Value?.ToString().ToLower();//F
        String segment = worksheet.Cells[row, 7].Value?.ToString().ToLower();//G
        String product = worksheet.Cells[row, 8].Value?.ToString().ToLower();//G
         int customerID = (int)Convert.ToInt64(worksheet.Cells[row, 9].Value?.ToString());//I
          String customerName = worksheet.Cells[row, 12].Value?.ToString();//L
        String customerGender = worksheet.Cells[row, 13].Value?.ToString();//M
        String nationality = worksheet.Cells[row, 63].Value?.ToString().ToLower();//BK
          String Cnic = worksheet.Cells[row, 64].Value?.ToString();//BL
        String Cnicexpiry = worksheet.Cells[row, 65].Value?.ToString();//BM
        String dob = worksheet.Cells[row, 66].Value?.ToString();//BN
        String Passport = worksheet.Cells[row, 67].Value?.ToString();//BO
        String Passportexpiry = worksheet.Cells[row, 68].Value?.ToString();//BP
        String email = worksheet.Cells[row, 70].Value?.ToString();//BR

          var userChk = new User
          {
            UserId = customerID,
            Uname = customerName,
            UserName = customerName+"-"+customerID,
            Password = customerName,
            Gender = customerGender,
            Cnic = Cnic,
            Cnicexpiry = Cnicexpiry,
            Passport = Passport,
            Passportexpiry = Passportexpiry,
            Dob = dob,
            RoleIdFk=2
          };

          int cus=GetOrCreateUserId(userChk);

          var offChk = new User
          {
            UserId = Convert.ToInt32(worksheet.Cells[row, 2].Value?.ToString()),//B
            UserName = "",
            Password = "",
            Gender = "N/A",
            Dob = "N/a",
            RoleIdFk = 5
          };
          int off=GetOrCreateUserId(offChk);
          GetOrCreateNationalityId(nationality,customerID);

          if (autoCempaign=="on")
          {
            int listId = GetOrCreateCampaignsIdAndUser(off, id,count);
            int listUserId = GetOrCreateCampaignsIdAndUserInsertion(listId, cus);
            count++;
          }

          excelDataList.Add(new Account
          {
            CustomerIdFk = cus,
            AllocationDate = worksheet.Cells[row, 1].Value?.ToString(),//A
            Center = center,
            UserIdFk = userId,
            Product = product,//H
            Customeracnumber = worksheet.Cells[row, 10].Value?.ToString(),//J
            ContractDate = worksheet.Cells[row, 11].Value?.ToString(),//K
            CmMobile = worksheet.Cells[row, 14].Value?.ToString(),//N
            CmHome = worksheet.Cells[row, 15].Value?.ToString(),//O
            CmWork = worksheet.Cells[row, 16].Value?.ToString(),//P
            Faxno = worksheet.Cells[row, 17].Value?.ToString(),//Q
            CmRef1 = worksheet.Cells[row, 18].Value?.ToString(),//R
            CmRef2 = worksheet.Cells[row, 19].Value?.ToString(),//S
            ContactPersonOrDeptt = worksheet.Cells[row, 20].Value?.ToString(),//T
            CmHomeaddress = worksheet.Cells[row, 21].Value?.ToString(),//U
            CmWorkaddress = worksheet.Cells[row, 22].Value?.ToString(),//V
            Outstandingbalance = Convert.ToInt32(worksheet.Cells[row, 23].Value?.ToString()),//W
            Contractamount = Convert.ToInt32(worksheet.Cells[row, 24].Value?.ToString()),//X
            Overdueamount = Convert.ToInt32(worksheet.Cells[row, 25].Value?.ToString()),//Y
            Minimumpayment = Convert.ToInt32(worksheet.Cells[row, 26].Value?.ToString()),//Z
            Openingbalance = Convert.ToInt32(worksheet.Cells[row, 27].Value?.ToString()),//AA
            Principleamount = Convert.ToInt32(worksheet.Cells[row, 28].Value?.ToString()),//AB
            Legalstatus = worksheet.Cells[row, 29].Value?.ToString(),//AC
            Cycledate = worksheet.Cells[row, 30].Value?.ToString(),//AD
            Bkt1 = Convert.ToInt32(worksheet.Cells[row, 31].Value?.ToString()),//AE
            Bkt2 = Convert.ToInt32(worksheet.Cells[row, 32].Value?.ToString()),//AF
            Bkt3 = Convert.ToInt32(worksheet.Cells[row, 33].Value?.ToString()),//AG
            Bkt4 = Convert.ToInt32(worksheet.Cells[row, 34].Value?.ToString()),//AH
            Bkt5 = Convert.ToInt32(worksheet.Cells[row, 35].Value?.ToString()),//AI
            Bkt6 = Convert.ToInt32(worksheet.Cells[row, 36].Value?.ToString()),//AJ
            Chargeoffamount = Convert.ToInt32(worksheet.Cells[row, 37].Value?.ToString()),//AK
            Chargeoffdate = worksheet.Cells[row, 38].Value?.ToString(),//AL
            Writtenoffamount = Convert.ToInt32(worksheet.Cells[row, 39].Value?.ToString()),//AM
            Writtenoffdate = worksheet.Cells[row, 40].Value?.ToString(),//AN
            Lastpayment = Convert.ToInt32(worksheet.Cells[row, 41].Value?.ToString()),//AO
            Lastpaymentdate = worksheet.Cells[row, 42].Value?.ToString(),//AP
            Secondlastpayment = Convert.ToInt32(worksheet.Cells[row, 43].Value?.ToString()),//AQ
            Secondlastpaymentdate = worksheet.Cells[row, 44].Value?.ToString(),//AR
            Thirdlastpayment = Convert.ToInt32(worksheet.Cells[row, 45].Value?.ToString()),//AS
            ThirdlastpaymentDate = worksheet.Cells[row, 46].Value?.ToString(),//AT
            Fourthlastpayment = Convert.ToInt32(worksheet.Cells[row, 47].Value?.ToString()),//AU
            FourthlastpaymentDate = worksheet.Cells[row, 48].Value?.ToString(),//AV
            Fifthlastpayment = Convert.ToInt32(worksheet.Cells[row, 49].Value?.ToString()),//AW
            FifthlastpaymentDate = worksheet.Cells[row, 50].Value?.ToString(),//AX
            Sixthlastpayment = Convert.ToInt32(worksheet.Cells[row, 51].Value?.ToString()),//AY
            SixthlastpaymentDate = worksheet.Cells[row, 52].Value?.ToString(),//AZ
            TotalClaimedAmount = Convert.ToInt32(worksheet.Cells[row, 53].Value?.ToString()),//BA
            TotalPaidAmount = Convert.ToInt32(worksheet.Cells[row, 54].Value?.ToString()),//BB
            UnpaidInvoice = Convert.ToInt32(worksheet.Cells[row, 55].Value?.ToString()),//BC
            Dpd = Convert.ToInt32(worksheet.Cells[row, 56].Value?.ToString()),//BD
            Dpd06 = Convert.ToInt32(worksheet.Cells[row, 57].Value?.ToString()),//BE
            Dpd712 = Convert.ToInt32(worksheet.Cells[row, 58].Value?.ToString()),//BF
            Dpd1318 = Convert.ToInt32(worksheet.Cells[row, 59].Value?.ToString()),//BG
            Dpd1924 = Convert.ToInt32(worksheet.Cells[row, 60].Value?.ToString()),//BH
            Dpd2536 = Convert.ToInt32(worksheet.Cells[row, 61].Value?.ToString()),//BI
            Dpd37plus = Convert.ToInt32(worksheet.Cells[row, 62].Value?.ToString()),//BJ
            Poboxno = worksheet.Cells[row, 69].Value?.ToString(),//BQ
            Specialinfo = worksheet.Cells[row, 71].Value?.ToString(),//BS
            Status = worksheet.Cells[row, 72].Value?.ToString(),//BT
            Remarks = worksheet.Cells[row, 73].Value?.ToString(),//BU
            OrganizationIdFk = Convert.ToInt32(GetOrCreateOrganizationId(organization)),
            CountryIdFk = Convert.ToInt32(GetOrCreateCountryId(country)),
            RegionIdFk = Convert.ToInt32(GetOrCreateRegionId(region)),
            SegmentIdFk = Convert.ToInt32(GetOrCreateSegmentId(segment)),
            EmailIdFk = Convert.ToInt32(GetOrCreateEmailId(email)),
            ProductIdFk = Convert.ToInt32(GetOrCreateProductId(product)),
            CenterIdFk = Convert.ToInt32(GetOrCreateCenterId(center)),
            CreatedAt = DateTime.UtcNow.ToString(),
            UpdatedAt = DateTime.UtcNow,
            Bucket = 0,
          }) ;
      }
      }
    }

    return excelDataList;
  }

  public int GetOrCreateUserId(User U)
  {
    // Check if the organization exists in the database
    var user = _db.Users
        .FirstOrDefault(o => o.UserId == U.UserId);

    if (user != null)
    {
      // Return the ID of the existing organization
      return user.UserId;
    }
    else
    {
      U.UpdatedAt = DateTime.UtcNow;
        U.CreatedAt = DateTime.UtcNow;
      _db.Users.Add(U);
      _db.SaveChanges();

      // Return the ID of the newly created organization
      return U.UserId;
    }
  }

  public int GetOrCreateNationalityId(string nationality,int userId)
  {
    // Check if the organization exists in the database
    var chk = _db.Nationalities
        .FirstOrDefault(o => o.Nationality1 == nationality && o.UserIdFk == userId);

    if (chk != null)
    {
      // Return the ID of the existing organization
      return chk.NationalityId;
    }
    else
    {
      // Create a new organization and insert it into the database
      var newNationality = new Nationality
      {
        Nationality1 = nationality,
        UserIdFk = userId,
        UpdatedAt = DateTime.UtcNow,
        CreatedAt = DateTime.UtcNow
    };

      _db.Nationalities.Add(newNationality);
      _db.SaveChanges();

      // Return the ID of the newly created organization
      return newNationality.NationalityId;
    }
  }

  public int GetOrCreateCampaignsIdAndUserInsertion(int list,int user)
  {
    
      // Create a new organization and insert it into the database
      var newcamlistUser = new Campaignslistuser
      {
        CampaignslistsIdFk = list,
        UserIdFk = user,
        Status = 1,
        UpdatedAt = DateTime.UtcNow,
        CreatedAt = DateTime.UtcNow
      };

      _db.Campaignslistusers.Add(newcamlistUser);
      _db.SaveChanges();

      // Return the ID of the newly created organization
      return newcamlistUser.CampaignslistusersId;
  }

  public int GetOrCreateCampaignsIdAndUser(int officer,int orgId, int count)
  {
    // Check if the organization exists in the database
    var c = _db.Campaignslists
        .FirstOrDefault(o => o.UserIdFk == officer && o.CampaignIdFk == orgId);

    if (c != null)
    {
      // Return the ID of the existing organization
      return c.CampaignslistsId;
    }
    else
    {
      // Create a new organization and insert it into the database
      var newcamlist = new Campaignslist
      {
        CampaignslistsName = "list-"+count,
        UserIdFk= officer,
        CampaignIdFk = orgId,
        Status = 1,
        UpdatedAt = DateTime.UtcNow,
        CreatedAt = DateTime.UtcNow
      };

      _db.Campaignslists.Add(newcamlist);
      _db.SaveChanges();

      // Return the ID of the newly created organization
      return newcamlist.CampaignslistsId;
    }
  }

  public int GetOrCreateCenterId(string centerr)
  {
    // Check if the center exists in the database
    var center = _db.Centers
        .FirstOrDefault(o => o.Cname == centerr);

    if (center != null)
    {
      // Return the ID of the existing center
      return center.CenterId;
    }
    else
    {
      // Create a new center and insert it into the database
      var newCenter = new Center
      {
        Cname = centerr,
      };

      _db.Centers.Add(newCenter);
      _db.SaveChanges();

      // Return the ID of the newly created center
      return newCenter.CenterId;
    }
  }

  public int GetOrCreateActionId(string act)
  {
    // Check if the center exists in the database
    var chk = _db.Actioncodes
        .FirstOrDefault(o => o.Actioncode1 == act);

    if (chk != null)
    {
      // Return the ID of the existing center
      return chk.ActionId;
    }
    else
    {
      // Create a new center and insert it into the database
      var newact = new Actioncode
      {
        Actioncode1 = act,
      };

      _db.Actioncodes.Add(newact);
      _db.SaveChanges();

      // Return the ID of the newly created center
      return newact.ActionId;
    }
  }

  public int GetOrCreateStatusId(string stat)
  {
    // Check if the center exists in the database
    var chk = _db.Statuscodes
        .FirstOrDefault(o => o.Statuscode1 == stat);

    if (chk != null)
    {
      // Return the ID of the existing center
      return chk.StatusId;
    }
    else
    {
      // Create a new center and insert it into the database
      var newstat = new Statuscode
      {
        Statuscode1 = stat,
      };

      _db.Statuscodes.Add(newstat);
      _db.SaveChanges();

      // Return the ID of the newly created center
      return newstat.StatusId;
    }
  }

  public int GetOrCreateProductId(string pro)
  {
    // Check if the product exists in the database
    var pros = _db.Products
        .FirstOrDefault(o => o.Pname == pro);

    if (pros != null)
    {
      // Return the ID of the existing product
      return pros.ProductId;
    }
    else
    {
      // Create a new product and insert it into the database
      var newProduct = new Product
      {
        Pname = pro
      };

      _db.Products.Add(newProduct);
      _db.SaveChanges();

      // Return the ID of the newly created product
      return newProduct.ProductId;
    }
  }

  public int GetOrCreateCampaignsId(string name,string desc)
  {
    // Check if the organization exists in the database
    var c = _db.Campaigns
        .FirstOrDefault(o => o.CampaignName == name);

    if (c != null)
    {
      // Return the ID of the existing organization
      return c.CampaignId;
    }
    else
    {
      // Create a new organization and insert it into the database
      var newcam = new Campaign
      {
        CampaignName = name.ToLower(),
        CampaignDesc = desc,
        Status = 1,
        UpdatedAt = DateTime.UtcNow,
        CreatedAt = DateTime.UtcNow
      };

      _db.Campaigns.Add(newcam);
      _db.SaveChanges();

      // Return the ID of the newly created organization
      return newcam.CampaignId;
    }
  }

  public int GetOrCreateEmailId(string emaill)
  {
    // Check if the organization exists in the database
    var email = _db.Emails
        .FirstOrDefault(o => o.Email1 == emaill);

    if (email != null)
    {
      // Return the ID of the existing organization
      return email.EmailId;
    }
    else
    {
      // Create a new organization and insert it into the database
      var newEmail = new Email
      {
        Email1 = emaill,
        UpdatedAt = DateTime.UtcNow,
        CreatedAt = DateTime.UtcNow
      };

      _db.Emails.Add(newEmail);
      _db.SaveChanges();

      // Return the ID of the newly created organization
      return newEmail.EmailId;
    }
  }

  public int GetOrCreateOrganizationId(string organizationName)
  {
    // Check if the organization exists in the database
    var organization = _db.Organizations
        .FirstOrDefault(o => o.Organizationname == organizationName);

    if (organization != null)
    {
      // Return the ID of the existing organization
      return organization.Organizationid;
    }
    else
    {
      // Create a new organization and insert it into the database
      var newOrganization = new Organization
      {
        Organizationname = organizationName
      };

      _db.Organizations.Add(newOrganization);
      _db.SaveChanges();

      // Return the ID of the newly created organization
      return newOrganization.Organizationid;
    }
  }

  public int GetOrCreateCountryId(string countryName)
  {
    // Check if the country exists in the database
    var country = _db.Countries
        .FirstOrDefault(o => o.Countryname == countryName);

    if (country != null)
    {
      // Return the ID of the existing country
      return country.Countryid;
    }
    else
    {
      // Create a new country and insert it into the database
      var newCountry = new Country
      {
        Countryname = countryName
      };

      _db.Countries.Add(newCountry);
      _db.SaveChanges();

      // Return the ID of the newly created country
      return newCountry.Countryid;
    }
  }

  public int GetOrCreateRegionId(string regionName)
  {
    // Check if the region exists in the database
    var region = _db.Regions
        .FirstOrDefault(r => r.Regionname == regionName);

    if (region != null)
    {
      // Return the ID of the existing region
      return region.Regionid;
    }
    else
    {
      // Create a new region and insert it into the database
      var newRegion = new AspnetCoreMvcFull.Models.Region
      {
        Regionname = regionName
      };

      _db.Regions.Add(newRegion);
      _db.SaveChanges();

      // Return the ID of the newly created region
      return newRegion.Regionid;
    }
  }

  public int GetOrCreateSegmentId(string segmentName)
  {
    // Check if the segment exists in the database
    var segment = _db.Segments
        .FirstOrDefault(s => s.Segmentname == segmentName);

    if (segment != null)
    {
      // Return the ID of the existing segment
      return segment.Segmentid;
    }
    else
    {
      // Create a new segment and insert it into the database
      var newSegment = new Segment
      {
        Segmentname = segmentName
      };

      _db.Segments.Add(newSegment);
      _db.SaveChanges();

      // Return the ID of the newly created segment
      return newSegment.Segmentid;
    }
  }
  private (int totalRows, int insertedRows, int updatedRows) InsertDataToDatabase(List<Account> excelDataList)
  {
    int insertedRows = 0;
    int updatedRows = 0;

    foreach (var acc in excelDataList)
    {
      //var existingAccount = _db.Accounts.FirstOrDefault(m => m.Accountid == acc.Accountid && m.UserIdFk==acc.UserIdFk);

      //if (false)
      //if (existingAccount != null)
      //{
      //  // Account already exists, update the existing record
      //  existingAccount.Lastpayment = acc.Lastpayment;
      //  existingAccount.Lastpaymentdate = acc.Lastpaymentdate;
      //  existingAccount.CmMobile = acc.CmMobile;
      //  existingAccount.CmHome = acc.CmHome;
      //  existingAccount.CmWork = acc.CmWork;
      //  existingAccount.CmRef1 = acc.CmRef1;
      //  existingAccount.CmRef2 = acc.CmRef2;
      //  existingAccount.CmHomeaddress = acc.CmHomeaddress;
      //  existingAccount.CmWorkaddress = acc.CmWorkaddress;
      //  existingAccount.Outstandingbalance = acc.Outstandingbalance;
      //  existingAccount.Contractamount = acc.Contractamount;
      //  existingAccount.Overdueamount = acc.Overdueamount;
      //  existingAccount.Minimumpayment = acc.Minimumpayment;
      //  existingAccount.Openingbalance = acc.Openingbalance;
      //  existingAccount.Principleamount = acc.Principleamount;
      //  existingAccount.Legalstatus = acc.Legalstatus;
      //  existingAccount.Bucket = acc.Bucket;
      //  existingAccount.Bkt1 = acc.Bkt1;
      //  existingAccount.Bkt2 = acc.Bkt2;
      //  existingAccount.Bkt3 = acc.Bkt3;
      //  existingAccount.Bkt4 = acc.Bkt4;
      //  existingAccount.Bkt5 = acc.Bkt5;
      //  existingAccount.Chargeoffamount = acc.Chargeoffamount;
      //  existingAccount.Chargeoffdate = acc.Chargeoffdate;
      //  existingAccount.Writtenoffamount = acc.Writtenoffamount;
      //  existingAccount.Writtenoffdate = acc.Writtenoffdate;
      //  existingAccount.Lastpaymentdate = acc.Lastpaymentdate;
      //  existingAccount.Dpd = acc.Dpd;
      //  existingAccount.Dpd06 = acc.Dpd06;
      //  existingAccount.Dpd712 = acc.Dpd712;
      //  existingAccount.Dpd1318 = acc.Dpd1318;
      //  existingAccount.Dpd37plus = acc.Dpd37plus;
      //  existingAccount.Poboxno = acc.Poboxno;
      //  existingAccount.EmailIdFk = acc.EmailIdFk;
      //  existingAccount.Specialinfo = acc.Specialinfo;
      //  existingAccount.Remarks = acc.Remarks;
      //  //existingAccount.Status = acc.Status;
      //  existingAccount.UpdatedAt = DateTime.UtcNow;
      //  _db.SaveChanges();

      //  if (int.Parse(acc.Status) == 1 || int.Parse(acc.Status) == 0)
      //  {
      //    //DateTime lastWeekStart = DateTime.Now.Date.AddDays(-6);
      //    //DateTime lastWeekEnd = DateTime.Now.Date.AddDays(1);

      //    //        var menuImg = _db.MenuImgs.Include(x => x.MenuIdFkNavigation).ThenInclude(x => x.CatIdFkNavigation)
      //    //.Where(x => x.MenuIdFk == existingMenu.MenuId && x.Status == 0 && x.MenuIdFkNavigation.Status == 1 && x.UpdatedAt >= lastWeekStart && x.UpdatedAt <= lastWeekEnd)
      //    //.ToList();

      //    //        foreach (var x in menuImg)
      //    //        {
      //    //          x.Status = 1;
      //    //          x.UpdatedAt = DateTime.Now;
      //    //          _db.MenuImgs.Update(x);
      //    //          _db.SaveChanges();
      //    //        }
      //  }
      //  else if (int.Parse(acc.Status) == -2)
      //  {
      //    //        var menuImg = _db.MenuImgs.Include(x => x.MenuIdFkNavigation).ThenInclude(x => x.CatIdFkNavigation)
      //    //.Where(x => x.MenuIdFk == existingMenu.MenuId && x.Status == 1 && x.MenuIdFkNavigation.Status == -2)
      //    //.ToList();

      //    //        foreach (var x in menuImg)
      //    //        {
      //    //          x.Status = 0;
      //    //          x.UpdatedAt = DateTime.Now;
      //    //          _db.MenuImgs.Update(x);
      //    //          _db.SaveChanges();
      //    //        }
      //  }

      //  updatedRows++;
      //}
      //else
      //{
        try
        {
          // acc doesn't exist, insert a new record
          acc.Status = "1";
          acc.UpdatedAt = DateTime.UtcNow;
          _db.Accounts.Add(acc);
          _db.SaveChanges();
          insertedRows++;
        }
        catch (Exception ex)
        {
          ViewBag.err(ex);

        }

        
        
      //}
    }



    int totalRows = insertedRows + updatedRows;
    return (totalRows, insertedRows, updatedRows);
  }
  public async Task<IActionResult> SampleFile(int? id)
  {
    if (id == null)
    {
      return NotFound();
    }

    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

    using (var package = new ExcelPackage())
    {
      //var worksheet = package.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "Sample");

      //if (worksheet == null)
      //{
      // Set the column headers
      var worksheet = package.Workbook.Worksheets.Add("Sample");
      worksheet.Cells[1, 1].Value = "Menu Name";
      worksheet.Cells[1, 2].Value = "Category";
      worksheet.Cells[1, 3].Value = "Price";
      worksheet.Cells[1, 4].Value = "Unit";
      worksheet.Cells[1, 5].Value = "No. of People";
      worksheet.Cells[1, 6].Value = "Ingredients";
      worksheet.Cells[1, 7].Value = "Description";
      worksheet.Cells[1, 8].Value = "Status";
      //}



      worksheet.Cells[1, 10].Value = "Status ID";
      worksheet.Cells[1, 11].Value = "Status Name";
      worksheet.Cells[1, 12].Value = "Color Configuration";
      worksheet.Cells[2, 10].Value = 1;
      worksheet.Cells[2, 11].Value = "Active";
      worksheet.Cells[2, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
      worksheet.Cells[2, 12].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
      worksheet.Cells[3, 10].Value = 0;
      worksheet.Cells[3, 11].Value = "In Recycle Bin";
      worksheet.Cells[3, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
      worksheet.Cells[3, 12].Style.Fill.BackgroundColor.SetColor(Color.MediumVioletRed);
      worksheet.Cells[4, 10].Value = -2;
      worksheet.Cells[4, 11].Value = "De-Active";
      worksheet.Cells[4, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
      worksheet.Cells[4, 12].Style.Fill.BackgroundColor.SetColor(Color.Red);



      // Set the column headers for category information
      worksheet.Cells[6, 10].Value = "Category ID";
      worksheet.Cells[6, 11].Value = "Category Name";
      worksheet.Cells[6, 12].Value = "Category Description";

      // Fetch the category data from the database
      //var categories = await _db.Categories.Where(a => a.BranchIdFk == id).ToListAsync();

      //// Populate the category information in the worksheet
      //for (int row = 7; row <= categories.Count + 1; row++)
      //{
      //  var category = categories[row - 7];

      //  worksheet.Cells[row, 10].Value = category.CatId;
      //  worksheet.Cells[row, 11].Value = category.CatName;
      //  worksheet.Cells[row, 12].Value = category.CatDescription;
      //}

      // Auto-fit the columns for better display
      worksheet.Cells.AutoFitColumns();

      // Save the changes to the existing Excel file
      package.Save();

      // Create a memory stream to store the Excel file
      var stream = new MemoryStream(package.GetAsByteArray());

      // Set the position to the beginning of the stream
      stream.Position = 0;

      // Return the Excel file as a downloadable attachment
      return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Sample.xlsx");
    }
  }

  //public async Task<IActionResult> ExportToExcel(int? id)
  //{
  //  if (id == null)
  //  {
  //    return NotFound();
  //  }

  //  var menus = _db.Accounts.Include(m => m.CatIdFkNavigation)
  //      .Where(c => c.Status != 0 && c.CatIdFkNavigation.BranchIdFk == id)
  //      .OrderBy(a => a.CreatedAt)
  //      .ToList();

  //  var lastWeekStart = DateTime.Now.Date.AddDays(-6);
  //  var lastWeekEnd = DateTime.Now.Date.AddDays(1);

  //  var deletedMenus = await _db.Accounts.Include(x => x.CatIdFkNavigation)
  //      .Where(x => x.Status == 0 && x.CatIdFkNavigation.Status == 1 && x.UpdatedAt >= lastWeekStart && x.UpdatedAt <= lastWeekEnd)
  //      .ToListAsync();

  //  ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

  //  //using (var package = new ExcelPackage(new FileInfo("Menus.xlsx")))
  //  //{
  //  //var worksheet = package.Workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == "Menus");
  //  //var worksheet = package.Workbook.Worksheets.Add("Menus");
  //  //if (worksheet == null)
  //  //{
  //  using (var package = new ExcelPackage())
  //  {
  //    var worksheet = package.Workbook.Worksheets.Add("Menus");
  //    // Set the column headers
  //    worksheet.Cells[1, 1].Value = "Menu Name";
  //    worksheet.Cells[1, 2].Value = "Category";
  //    worksheet.Cells[1, 3].Value = "Price";
  //    worksheet.Cells[1, 4].Value = "Unit";
  //    worksheet.Cells[1, 5].Value = "No. of People";
  //    worksheet.Cells[1, 6].Value = "Ingredients";
  //    worksheet.Cells[1, 7].Value = "Description";
  //    worksheet.Cells[1, 8].Value = "Status";
  //    //}

  //    // Get the next available row for appending data
  //    var lastUsedRow = worksheet.Dimension?.End.Row ?? 1;
  //    var nextRow = lastUsedRow + 1;

  //    // Populate the rows with data
  //    foreach (var menu in menus)
  //    {
  //      worksheet.Cells[nextRow, 1].Value = menu.MenuName;
  //      worksheet.Cells[nextRow, 2].Value = menu.CatIdFk;
  //      worksheet.Cells[nextRow, 3].Value = menu.Price;
  //      worksheet.Cells[nextRow, 4].Value = menu.Unit;
  //      worksheet.Cells[nextRow, 5].Value = menu.NoOfPeople;
  //      worksheet.Cells[nextRow, 6].Value = menu.Ingredients;
  //      worksheet.Cells[nextRow, 7].Value = menu.Description;
  //      worksheet.Cells[nextRow, 8].Value = menu.Status;

  //      // Apply status color formatting
  //      if (menu.Status == 1)
  //      {
  //        worksheet.Cells[nextRow, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
  //        worksheet.Cells[nextRow, 8].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
  //      }
  //      else if (menu.Status == -2)
  //      {
  //        worksheet.Cells[nextRow, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
  //        worksheet.Cells[nextRow, 8].Style.Fill.BackgroundColor.SetColor(Color.MediumVioletRed);
  //      }

  //      nextRow++;
  //    }

  //    // Append the deleted menus
  //    foreach (var deletedMenu in deletedMenus)
  //    {
  //      worksheet.Cells[nextRow, 1].Value = deletedMenu.MenuName;
  //      worksheet.Cells[nextRow, 2].Value = deletedMenu.CatIdFk;
  //      worksheet.Cells[nextRow, 3].Value = deletedMenu.Price;
  //      worksheet.Cells[nextRow, 4].Value = deletedMenu.Unit;
  //      worksheet.Cells[nextRow, 5].Value = deletedMenu.NoOfPeople;
  //      worksheet.Cells[nextRow, 6].Value = deletedMenu.Ingredients;
  //      worksheet.Cells[nextRow, 7].Value = deletedMenu.Description;
  //      worksheet.Cells[nextRow, 8].Value = deletedMenu.Status;

  //      // Apply status color formatting
  //      worksheet.Cells[nextRow, 1, nextRow, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
  //      worksheet.Cells[nextRow, 1, nextRow, 8].Style.Fill.BackgroundColor.SetColor(Color.Red);

  //      nextRow++;
  //    }

  //    worksheet.Cells[1, 10].Value = "Status ID";
  //    worksheet.Cells[1, 11].Value = "Status Name";
  //    worksheet.Cells[1, 12].Value = "Color Configuration";
  //    worksheet.Cells[2, 10].Value = 1;
  //    worksheet.Cells[2, 11].Value = "Active";
  //    worksheet.Cells[2, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
  //    worksheet.Cells[2, 12].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
  //    worksheet.Cells[3, 10].Value = 0;
  //    worksheet.Cells[3, 11].Value = "In Recycle Bin";
  //    worksheet.Cells[3, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
  //    worksheet.Cells[3, 12].Style.Fill.BackgroundColor.SetColor(Color.MediumVioletRed);
  //    worksheet.Cells[4, 10].Value = -2;
  //    worksheet.Cells[4, 11].Value = "De-Active";
  //    worksheet.Cells[4, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
  //    worksheet.Cells[4, 12].Style.Fill.BackgroundColor.SetColor(Color.Red);



  //    // Set the column headers for category information
  //    worksheet.Cells[6, 10].Value = "Category ID";
  //    worksheet.Cells[6, 11].Value = "Category Name";
  //    worksheet.Cells[6, 12].Value = "Category Description";

  //    // Fetch the category data from the database
  //    //var categories = await _db.Categories.Where(a => a.BranchIdFk == id).ToListAsync();

  //    //// Populate the category information in the worksheet
  //    //for (int row = 7; row <= categories.Count + 1; row++)
  //    //{
  //    //  var category = categories[row - 7];

  //    //  worksheet.Cells[row, 10].Value = category.CatId;
  //    //  worksheet.Cells[row, 11].Value = category.CatName;
  //    //  worksheet.Cells[row, 12].Value = category.CatDescription;
  //    //}

  //    // Auto-fit the columns for better display
  //    worksheet.Cells.AutoFitColumns();

  //    // Save the changes to the existing Excel file
  //    package.Save();

  //    // Create a memory stream to store the Excel file
  //    var stream = new MemoryStream(package.GetAsByteArray());

  //    // Set the position to the beginning of the stream
  //    stream.Position = 0;

  //    // Return the Excel file as a downloadable attachment
  //    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Menus.xlsx");
  //  }
  //}
}
