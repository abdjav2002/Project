using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Profile
{
  public int ProfileId { get; set; }

  public string Imagepath { get; set; } = null!;

  public int? UserIdFk { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public DateTime? CreatedAt { get; set; }

  public virtual User? UserIdFkNavigation { get; set; }
}
