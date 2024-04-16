using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Campaign
{
  public int CampaignId { get; set; }

  public string CampaignName { get; set; } = null!;

  public string CampaignDesc { get; set; } = null!;

  public int? Status { get; set; }

  public DateTime? CreatedAt { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public virtual ICollection<Campaignslist> Campaignslists { get; set; } = new List<Campaignslist>();
}
