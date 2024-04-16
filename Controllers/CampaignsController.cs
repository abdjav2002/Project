using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Controllers
{
  public class CampaignsController : BaseController
  {
    public CampaignsController(DebtsyncContext obj) : base(obj)
    {
    }

    public async Task<IActionResult> DNC()
    {
      var UserDetail = _db.Users.Include(x => x.RoleIdFkNavigation)
        .Include(c => c.CreatedbyIdFkNavigation)
        .Include(f => f.CreatedtoIdFkNavigation)
        .Where(a => a.RoleIdFk == 2 && a.Status==-1).ToList();

      return View(UserDetail);
    }

    public async Task<IActionResult> Restore(int id)
    {
      var uDetail = await _db.Users
          .FirstOrDefaultAsync(c => c.UserId == id);
      if (uDetail != null)
      {
        // Update the status
        uDetail.Status = -1;

        // Optionally, update the UpdatedAt timestamp
        uDetail.UpdatedAt = DateTime.UtcNow;

        // Save changes to the database
        await _db.SaveChangesAsync();
      }
      return RedirectToAction(nameof(Details));
    }

    [HttpPost]
    public async Task<IActionResult> AssignedEdit(int option,int campaignslistuserId, int[] selectedIds)
    {
      // Retrieve the entity from the database

      if (campaignslistuserId != null)
      {
        foreach (var uid in selectedIds)
        {
      var campaignslistuser = await _db.Campaignslistusers
          .FirstOrDefaultAsync(c => c.CampaignslistsIdFk == campaignslistuserId && c.UserIdFk==uid);

          if (campaignslistuser != null)
          {
            // Update the status
            campaignslistuser.Status = option==1?0:-1;

            // Optionally, update the UpdatedAt timestamp
            campaignslistuser.UpdatedAt = DateTime.UtcNow;

            // Save changes to the database
            await _db.SaveChangesAsync();

            //DNC
            if (option == 2)
            {
              var uDetail = await _db.Users
          .FirstOrDefaultAsync(c => c.UserId == uid);
              if (uDetail != null)
              {
                // Update the status
                uDetail.Status = -1;

                // Optionally, update the UpdatedAt timestamp
                uDetail.UpdatedAt = DateTime.UtcNow;

                // Save changes to the database
                await _db.SaveChangesAsync();
              }
            }
          }
        }

        // Redirect on success
        return RedirectToAction(nameof(Detail));
      }
      else
      {
        // Handle case where the entity is not found
        throw new InvalidOperationException($"Campaignslistuser with ID {campaignslistuserId} not found.");
      }
      return View();
    }

    public async Task<IActionResult> AssignedEdit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      ViewBag.id = id;

      var x = _db.Campaignslistusers
        .Include(a => a.CampaignslistsIdFkNavigation)
        .Include(a => a.UserIdFkNavigation)
        .Where(a => a.CampaignslistsIdFk == id && a.Status != -1 && a.Status != 0).ToList();

      List<Account> acc = new List<Account>();

      foreach (var s in x)
      {
        var q = _db.Accounts.Where(e => e.CustomerIdFk == s.UserIdFk).FirstOrDefault();
        if (q != null)
        {
          acc.Add(q);
        }
      }

      return View(acc);
    }


    // GET: Campaigns/Create
    public async Task<IActionResult> Detail()
    {
      var list = _db.Campaigns.ToList();
      ViewBag.list = _db.Campaignslists.Include(a=>a.UserIdFkNavigation).ToList();
      ViewBag.listUsers = _db.Campaignslistusers.Include(a=>a.UserIdFkNavigation).Where(a=>a.Status!=-1 && a.Status != 0).ToList();
      ViewBag.accounts = _db.Accounts
        .Include(a => a.CountryIdFkNavigation)
        .Include(a => a.RegionIdFkNavigation)
        .Include(a => a.SegmentIdFkNavigation)
        .Include(a => a.OrganizationIdFkNavigation)
        .Include(a => a.EmailIdFkNavigation)
        .Include(a => a.CustomerIdFkNavigation)
        .Include(a => a.ProductIdFkNavigation)
        .Include(a => a.CenterIdFkNavigation)
        .Include(a => a.UserIdFkNavigation)
        .OrderByDescending(a => a.CreatedAt).ToList();
      return View(list);
    }

    // GET: Campaigns/Create
    public IActionResult CampaignsCreate()
    {
      return View();
    }

    // POST: Campaigns/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CampaignsCreate([Bind("CampaignName,CampaignDesc")] Campaign campaign)
    {
      if (ModelState.IsValid)
      {
        campaign.Status = 1;
        campaign.UpdatedAt = DateTime.UtcNow;
        campaign.CreatedAt = DateTime.UtcNow;
        _db.Add(campaign);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Detail));
      }
      return View(campaign);
    }

    // GET: Campaigns
    public async Task<IActionResult> Index()
    {
      return View(await _db.Campaigns.ToListAsync());
    }

    // GET: Campaigns/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var campaign = await _db.Campaigns.FindAsync(id);
      if (campaign == null)
      {
        return NotFound();
      }
      return View(campaign);
    }

    // POST: Campaigns/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CampaignId,CampaignName,CampaignDesc,Status,CreatedAt,UpdatedAt")] Campaign campaign)
    {
      if (id != campaign.CampaignId)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _db.Update(campaign);
          await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!CampaignExists(campaign.CampaignId))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(campaign);
    }

    // GET: Campaigns/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var campaign = await _db.Campaigns
          .FirstOrDefaultAsync(m => m.CampaignId == id);
      if (campaign == null)
      {
        return NotFound();
      }

      return View(campaign);
    }

    // POST: Campaigns/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var campaign = await _db.Campaigns.FindAsync(id);
      _db.Campaigns.Remove(campaign);
      await _db.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool CampaignExists(int id)
    {
      return _db.Campaigns.Any(e => e.CampaignId == id);
    }

    // GET: Campaignslists/Create
    public async Task<IActionResult> CampaignsListsCreate(int? id)
    {
      if(id == null){
        return NotFound();
      }
      // Retrieve the list of campaigns with status 1 from the database
      var campaigns = await _db.Campaigns.Where(c => c.Status == 1).ToListAsync();
      var fieldOfficer = await _db.Users.Where(u => u.RoleIdFk == 5).ToListAsync(); ;

      // Pass the filtered list of campaigns to the view using ViewBag
      ViewBag.Campaigns = campaigns;
      ViewBag.fieldOfficer = fieldOfficer;
      ViewBag.id = id;
      return View();
    }

    // POST: Campaignslists/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CampaignsListsCreate([Bind("UserIdFk,CampaignIdFk,CampaignslistsName")] Campaignslist campaignslist)
    {
      if (ModelState.IsValid)
      {
        campaignslist.Status = 1;
        campaignslist.UpdatedAt = DateTime.UtcNow;
        campaignslist.CreatedAt = DateTime.UtcNow;
        _db.Add(campaignslist);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Detail));
      }
      // Retrieve the list of campaigns with status 1 from the database
      var campaigns = await _db.Campaigns.Where(c => c.Status == 1).ToListAsync();

      // Pass the filtered list of campaigns to the view using ViewBag
      ViewBag.Campaigns = campaigns;
      return View(campaignslist);
    }

    // GET: Campaignslists/Edit/5
    public async Task<IActionResult> EditCampaignslist(int? id)
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
      return View(campaignslist);
    }

    // POST: Campaignslists/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCampaignslist(int id, [Bind("CampaignslistsId,CampaignIdFk,CampaignslistsName,Status,CreatedAt,UpdatedAt")] Campaignslist campaignslist)
    {
      if (id != campaignslist.CampaignslistsId)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _db.Update(campaignslist);
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
        return RedirectToAction(nameof(Index));
      }
      return View(campaignslist);
    }

    // GET: Campaignslists/Delete/5
    public async Task<IActionResult> DeleteCampaignslist(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var campaignslist = await _db.Campaignslists
          .FirstOrDefaultAsync(m => m.CampaignslistsId == id);
      if (campaignslist == null)
      {
        return NotFound();
      }

      return View(campaignslist);
    }

    // POST: Campaignslists/Delete/5
    [HttpPost, ActionName("DeleteCampaignslist")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedCampaignslist(int id)
    {
      var campaignslist = await _db.Campaignslists.FindAsync(id);
      _db.Campaignslists.Remove(campaignslist);
      await _db.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool CampaignslistExists(int id)
    {
      return _db.Campaignslists.Any(e => e.CampaignslistsId == id);
    }

    // GET: Campaignslistusers/Create
    public async Task<IActionResult> CampaignsListUsersCreate(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }
      ViewBag.id = id;
      // Retrieve the list of campaigns with status 1 from the database
      var campaignsLists = await _db.Campaignslists.Where(c => c.Status == 1).ToListAsync();

      // Retrieve all users with status 1 from the database
      var users = await _db.Users.Where(u => u.RoleIdFk == 2 && u.Status!=-1).ToListAsync();

      // Retrieve the list of users already included in CampaignsListUsers
      var usersIncludedInCampaignsListUsers = await _db.Campaignslistusers.Where(a=>a.Status!=0).Select(cu => cu.UserIdFk).ToListAsync();

      // Filter out users who are already included in CampaignsListUsers
      var filteredUsers = users.Where(u => !usersIncludedInCampaignsListUsers.Contains(u.UserId)).ToList();

      // Pass the filtered list of campaigns and users to the view using ViewBag
      ViewBag.CampaignsLists = campaignsLists;
      ViewBag.Users = filteredUsers;

      return View();
    }


    // POST: Campaignslistusers/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CampaignsListUsersCreate([Bind("CampaignslistsIdFk,UserIdFk")] Campaignslistuser campaignslistuser, int[] selectedIds)
    {
      if (ModelState.IsValid)
      {
        var campiagnsId = campaignslistuser.CampaignslistsIdFk;
        foreach (var id in selectedIds)
        {
          var obj = new Campaignslistuser
          {
            CampaignslistsIdFk = campiagnsId,
            UserIdFk = id,
            Status = 1,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
          };
          _db.Add(obj);
          await _db.SaveChangesAsync();
        }
        
        return RedirectToAction(nameof(Detail));
      }
      // Retrieve the list of campaigns with status 1 from the database
      var campaignsLists = await _db.Campaignslists.Where(c => c.Status == 1).ToListAsync();
      var users = await _db.Users.Where(c => c.Status == 2).ToListAsync();

      // Pass the filtered list of campaigns to the view using ViewBag
      ViewBag.CampaignsLists = campaignsLists;
      ViewBag.Users = users;
      return View(campaignslistuser);
    }

    // GET: Campaignslistusers/Edit/5
    public async Task<IActionResult> EditCampaignslistuser(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var campaignslistuser = await _db.Campaignslistusers.FindAsync(id);
      if (campaignslistuser == null)
      {
        return NotFound();
      }
      return View(campaignslistuser);
    }

    // POST: Campaignslistusers/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCampaignslistuser(int id, [Bind("CampaignslistusersId,CampaignslistsIdFk,UserIdFk,Status,CreatedAt,UpdatedAt")] Campaignslistuser campaignslistuser)
    {
      if (id != campaignslistuser.CampaignslistusersId)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _db.Update(campaignslistuser);
          await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!CampaignslistuserExists(campaignslistuser.CampaignslistusersId))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(campaignslistuser);
    }

    // GET: Campaignslistusers/Delete/5
    public async Task<IActionResult> DeleteCampaignslistuser(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var campaignslistuser = await _db.Campaignslistusers
          .FirstOrDefaultAsync(m => m.CampaignslistusersId == id);
      if (campaignslistuser == null)
      {
        return NotFound();
      }

      return View(campaignslistuser);
    }

    // POST: Campaignslistusers/Delete/5
    [HttpPost, ActionName("DeleteCampaignslistuser")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedCampaignslistuser(int id)
    {
      var campaignslistuser = await _db.Campaignslistusers.FindAsync(id);
      _db.Campaignslistusers.Remove(campaignslistuser);
      await _db.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool CampaignslistuserExists(int id)
    {
      return _db.Campaignslistusers.Any(e => e.CampaignslistusersId == id);
    }
  }
}
