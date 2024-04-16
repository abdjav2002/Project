using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Organization
{
  public int Organizationid { get; set; }

  public string? Organizationname { get; set; }

  public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
