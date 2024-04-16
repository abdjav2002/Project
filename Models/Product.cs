using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Product
{
  public int ProductId { get; set; }

  public string Pname { get; set; } = null!;

  public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
