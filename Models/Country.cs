using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Country
{
  public int Countryid { get; set; }

  public string? Countryname { get; set; }

  public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
