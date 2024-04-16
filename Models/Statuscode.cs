using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Statuscode
{
  public int StatusId { get; set; }

  public string? Statuscode1 { get; set; }

  public string? Sname { get; set; }

  public string? Sdescription { get; set; }

  public DateTimeOffset? CreatedAt { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public int? Amount { get; set; }

  public int? FollowupDate { get; set; }

  public string? Color { get; set; }

  public virtual ICollection<Remark> Remarks { get; set; } = new List<Remark>();
}
