using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Nationality
{
  public int NationalityId { get; set; }

  public string Nationality1 { get; set; } = null!;

  public int? UserIdFk { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public DateTime? CreatedAt { get; set; }

  public virtual User? UserIdFkNavigation { get; set; }
}
