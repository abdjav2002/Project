using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class User
{
  public int UserId { get; set; }

  public string UserName { get; set; } = null!;

  public string Dob { get; set; } = null!;

  public string Gender { get; set; } = null!;

  public string? Passport { get; set; }

  public string? Cnic { get; set; }

  public string? Cnicexpiry { get; set; }

  public string? Passportexpiry { get; set; }

  public int? RoleIdFk { get; set; }

  public int Status { get; set; }

  public DateTime? CreatedAt { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public string Password { get; set; } = null!;

  public int? CreatedbyIdFk { get; set; }

  public int? CreatedtoIdFk { get; set; }

  public string? Uname { get; set; }

  public virtual ICollection<Account> AccountCustomerIdFkNavigations { get; set; } = new List<Account>();

  public virtual ICollection<Account> AccountUserIdFkNavigations { get; set; } = new List<Account>();

  public virtual ICollection<Campaignslist> Campaignslists { get; set; } = new List<Campaignslist>();

  public virtual ICollection<Campaignslistuser> Campaignslistusers { get; set; } = new List<Campaignslistuser>();

  public virtual User? CreatedbyIdFkNavigation { get; set; }

  public virtual User? CreatedtoIdFkNavigation { get; set; }

  public virtual ICollection<User> InverseCreatedbyIdFkNavigation { get; set; } = new List<User>();

  public virtual ICollection<User> InverseCreatedtoIdFkNavigation { get; set; } = new List<User>();

  public virtual ICollection<Nationality> Nationalities { get; set; } = new List<Nationality>();

  public virtual ICollection<Profile> Profiles { get; set; } = new List<Profile>();

  public virtual ICollection<Remark> Remarks { get; set; } = new List<Remark>();

  public virtual Role? RoleIdFkNavigation { get; set; }
}
