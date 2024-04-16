using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Remark
{
  public int RemarkId { get; set; }

  public string? RemarkText { get; set; }

  public long? AccountIdFk { get; set; }

  public int? ActioncodeIdFk { get; set; }

  public int? StatuscodeIdFk { get; set; }

  public DateTime? CreatedAt { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public int? UserIdFk { get; set; }

  public int? PtpAmount { get; set; }

  public string? PtpDate { get; set; }

  public virtual Account? AccountIdFkNavigation { get; set; }

  public virtual Actioncode? ActioncodeIdFkNavigation { get; set; }

  public virtual Statuscode? StatuscodeIdFkNavigation { get; set; }

  public virtual User? UserIdFkNavigation { get; set; }
}
