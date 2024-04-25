using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Smsacc
{
  public int SmsAccId { get; set; }

  public long AccountIdFk { get; set; }

  public int SmsIdFk { get; set; }

  public DateTimeOffset CreatedAt { get; set; }

  public virtual Account AccountIdFkNavigation { get; set; } = null!;

  public virtual Smstext SmsIdFkNavigation { get; set; } = null!;
}
