using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Campaignslist
{
  public int CampaignslistsId { get; set; }

  public int CampaignIdFk { get; set; }

  public string? CampaignslistsName { get; set; }

  public int? Status { get; set; }

  public DateTime? CreatedAt { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public int? UserIdFk { get; set; }

  public int? Target { get; set; }

  public virtual Campaign? CampaignIdFkNavigation { get; set; } = null!;

  public virtual ICollection<Campaignslistuser> Campaignslistusers { get; set; } = new List<Campaignslistuser>();

  public virtual User? UserIdFkNavigation { get; set; }
}
