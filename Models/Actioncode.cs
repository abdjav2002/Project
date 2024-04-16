using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Actioncode
{
  public int ActionId { get; set; }

  public string? Actioncode1 { get; set; }

  public string? Aname { get; set; }

  public string? Adescription { get; set; }

  public DateTimeOffset? CreatedAt { get; set; }

  public DateTimeOffset? UpdatedAt { get; set; }

  public int? Amount { get; set; }

  public string? Color { get; set; }

  public int? FollowUpDate { get; set; }

  public virtual ICollection<Remark> Remarks { get; set; } = new List<Remark>();
}
