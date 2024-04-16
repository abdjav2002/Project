using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Region
{
  public int Regionid { get; set; }

  public string? Regionname { get; set; }

  public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
