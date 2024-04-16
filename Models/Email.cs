using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Email
{
  public int EmailId { get; set; }

  public string Email1 { get; set; } = null!;

  public DateTime? UpdatedAt { get; set; }

  public DateTime? CreatedAt { get; set; }

  public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
