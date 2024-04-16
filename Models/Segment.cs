using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Segment
{
  public int Segmentid { get; set; }

  public string? Segmentname { get; set; }

  public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
