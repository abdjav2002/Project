using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Smstext
{
  public int SmsTextId { get; set; }

  public string Sms { get; set; } = null!;

  public DateTime CreatedAt { get; set; }

  public virtual ICollection<Smsacc> Smsaccs { get; set; } = new List<Smsacc>();
}
