using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Payment
{
  public int PaymentId { get; set; }

  public double Payment1 { get; set; }

  public long? AccountIdFk { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public string? CreatedAt { get; set; }

  public virtual Account? AccountIdFkNavigation { get; set; }
}
