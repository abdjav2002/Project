using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Note
{
  public int NoteId { get; set; }

  public string? EmiratesId { get; set; }

  public string? SocOrRecWeb { get; set; }

  public string? CompanySearch { get; set; }

  public string? ImmigrationChk { get; set; }

  public string? EmailSearchOrBobox { get; set; }

  public string? NewContactOrAddress { get; set; }

  public string? SpecialInfo { get; set; }

  public string? Feedback { get; set; }

  public int? Status { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public DateTime? CreatedAt { get; set; }

  public long? AccountIdFk { get; set; }

  public virtual Account? AccountIdFkNavigation { get; set; }
}
