using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Emailacc
{
  public int EmailAccId { get; set; }

  public long AccountIdFk { get; set; }

  public int EmailtextIdFk { get; set; }

  public DateTimeOffset CreatedAt { get; set; }

  public virtual Account AccountIdFkNavigation { get; set; } = null!;

  public virtual Emailtext EmailtextIdFkNavigation { get; set; } = null!;
}
