using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Emailtext
{
  public int EmailTextId { get; set; }

  public string Email { get; set; } = null!;

  public DateTimeOffset CreatedAt { get; set; }

  public virtual ICollection<Emailacc> Emailaccs { get; set; } = new List<Emailacc>();
}
