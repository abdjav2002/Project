using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Campaignslistuser
{
  public int CampaignslistusersId { get; set; }

  public int CampaignslistsIdFk { get; set; }

  public int UserIdFk { get; set; }

  public int? Status { get; set; }

  public DateTime? CreatedAt { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public virtual Campaignslist? CampaignslistsIdFkNavigation { get; set; } = null!;

  public virtual User? UserIdFkNavigation { get; set; } = null!;
}
