using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Center
{
  public int CenterId { get; set; }

  public string Cname { get; set; } = null!;

  public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
