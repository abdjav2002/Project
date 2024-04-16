using System;
using System.Collections.Generic;

namespace AspnetCoreMvcFull.Models;

public partial class Account
{
  public long Accountid { get; set; }

  public int? UserIdFk { get; set; }

  public string? Product { get; set; }

  public string? Customeracnumber { get; set; }

  public string? CmMobile { get; set; }

  public string? CmHome { get; set; }

  public string? CmWork { get; set; }

  public string? CmRef1 { get; set; }

  public string? CmRef2 { get; set; }

  public string? CmHomeaddress { get; set; }

  public string? CmWorkaddress { get; set; }

  public decimal? Outstandingbalance { get; set; }

  public decimal? Contractamount { get; set; }

  public decimal? Overdueamount { get; set; }

  public decimal? Minimumpayment { get; set; }

  public decimal? Openingbalance { get; set; }

  public decimal? Principleamount { get; set; }

  public string? Legalstatus { get; set; }

  public string? Cycledate { get; set; }

  public int? Bucket { get; set; }

  public int? Bkt1 { get; set; }

  public int? Bkt2 { get; set; }

  public int? Bkt3 { get; set; }

  public int? Bkt4 { get; set; }

  public int? Bkt5 { get; set; }

  public int? Bkt6 { get; set; }

  public decimal? Chargeoffamount { get; set; }

  public string? Chargeoffdate { get; set; }

  public decimal? Writtenoffamount { get; set; }

  public string? Writtenoffdate { get; set; }

  public decimal? Lastpayment { get; set; }

  public string? Lastpaymentdate { get; set; }

  public int? Dpd { get; set; }

  public int? Dpd06 { get; set; }

  public int? Dpd712 { get; set; }

  public int? Dpd1318 { get; set; }

  public int? Dpd37plus { get; set; }

  public string? Poboxno { get; set; }

  public string? Specialinfo { get; set; }

  public string? Status { get; set; }

  public string? Remarks { get; set; }

  public DateTime? UpdatedAt { get; set; }

  public string? CreatedAt { get; set; }

  public int? OrganizationIdFk { get; set; }

  public int? CountryIdFk { get; set; }

  public int? RegionIdFk { get; set; }

  public int? SegmentIdFk { get; set; }

  public int? EmailIdFk { get; set; }

  public string? AllocationDate { get; set; }

  public string? ContractDate { get; set; }

  public string? Faxno { get; set; }

  public string? ContactPersonOrDeptt { get; set; }

  public string? Secondlastpaymentdate { get; set; }

  public decimal? Thirdlastpayment { get; set; }

  public string? ThirdlastpaymentDate { get; set; }

  public decimal? Fourthlastpayment { get; set; }

  public string? FourthlastpaymentDate { get; set; }

  public decimal? Fifthlastpayment { get; set; }

  public string? FifthlastpaymentDate { get; set; }

  public decimal? Sixthlastpayment { get; set; }

  public string? SixthlastpaymentDate { get; set; }

  public decimal? Secondlastpayment { get; set; }

  public decimal? TotalClaimedAmount { get; set; }

  public decimal? TotalPaidAmount { get; set; }

  public decimal? UnpaidInvoice { get; set; }

  public int? Dpd1924 { get; set; }

  public int? Dpd2536 { get; set; }

  public string? Center { get; set; }

  public int? CustomerIdFk { get; set; }

  public int? ProductIdFk { get; set; }

  public int? CenterIdFk { get; set; }

  public virtual Center? CenterIdFkNavigation { get; set; }

  public virtual Country? CountryIdFkNavigation { get; set; }

  public virtual User? CustomerIdFkNavigation { get; set; }

  public virtual Email? EmailIdFkNavigation { get; set; }

  public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

  public virtual Organization? OrganizationIdFkNavigation { get; set; }

  public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

  public virtual Product? ProductIdFkNavigation { get; set; }

  public virtual Region? RegionIdFkNavigation { get; set; }

  public virtual ICollection<Remark> RemarksNavigation { get; set; } = new List<Remark>();

  public virtual Segment? SegmentIdFkNavigation { get; set; }

  public virtual User? UserIdFkNavigation { get; set; }
}
