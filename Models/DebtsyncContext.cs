using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Models;

public partial class DebtsyncContext : DbContext
{
  public DebtsyncContext()
  {
  }

  public DebtsyncContext(DbContextOptions<DebtsyncContext> options)
      : base(options)
  {
  }

  public virtual DbSet<Account> Accounts { get; set; }

  public virtual DbSet<Actioncode> Actioncodes { get; set; }

  public virtual DbSet<Campaign> Campaigns { get; set; }

  public virtual DbSet<Campaignslist> Campaignslists { get; set; }

  public virtual DbSet<Campaignslistuser> Campaignslistusers { get; set; }

  public virtual DbSet<Center> Centers { get; set; }

  public virtual DbSet<Country> Countries { get; set; }

  public virtual DbSet<Email> Emails { get; set; }

  public virtual DbSet<Emailacc> Emailaccs { get; set; }

  public virtual DbSet<Emailtext> Emailtexts { get; set; }

  public virtual DbSet<Nationality> Nationalities { get; set; }

  public virtual DbSet<Note> Notes { get; set; }

  public virtual DbSet<Organization> Organizations { get; set; }

  public virtual DbSet<Payment> Payments { get; set; }

  public virtual DbSet<Product> Products { get; set; }

  public virtual DbSet<Region> Regions { get; set; }

  public virtual DbSet<Remark> Remarks { get; set; }

  public virtual DbSet<Role> Roles { get; set; }

  public virtual DbSet<Segment> Segments { get; set; }

  public virtual DbSet<Smsacc> Smsaccs { get; set; }

  public virtual DbSet<Smstext> Smstexts { get; set; }

  public virtual DbSet<Statuscode> Statuscodes { get; set; }

  public virtual DbSet<User> Users { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
      => optionsBuilder.UseNpgsql("Server=monorail.proxy.rlwy.net;Port=14788;Database=railway;Uid=postgres;Pwd=AAkaonmDsoevodbHWIsOzrTieOKBfczc;");

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Account>(entity =>
    {
      entity.HasKey(e => e.Accountid).HasName("accounts_pkey");

      entity.ToTable("accounts");

      entity.Property(e => e.Accountid)
              .HasDefaultValueSql("nextval('accounts_contractid_seq'::regclass)")
              .HasColumnName("accountid");
      entity.Property(e => e.AllocationDate).HasColumnType("character varying");
      entity.Property(e => e.Bkt1).HasColumnName("bkt1");
      entity.Property(e => e.Bkt2).HasColumnName("bkt2");
      entity.Property(e => e.Bkt3).HasColumnName("bkt3");
      entity.Property(e => e.Bkt4).HasColumnName("bkt4");
      entity.Property(e => e.Bkt5).HasColumnName("bkt5");
      entity.Property(e => e.Bkt6).HasColumnName("bkt6");
      entity.Property(e => e.Bucket).HasColumnName("bucket");
      entity.Property(e => e.Center)
              .HasColumnType("character varying")
              .HasColumnName("center");
      entity.Property(e => e.CenterIdFk).HasColumnName("center_id_fk");
      entity.Property(e => e.Chargeoffamount)
              .HasPrecision(10, 2)
              .HasColumnName("chargeoffamount");
      entity.Property(e => e.Chargeoffdate)
              .HasColumnType("character varying")
              .HasColumnName("chargeoffdate");
      entity.Property(e => e.CmHome)
              .HasMaxLength(255)
              .HasColumnName("cm_home");
      entity.Property(e => e.CmHomeaddress).HasColumnName("cm_homeaddress");
      entity.Property(e => e.CmMobile)
              .HasMaxLength(255)
              .HasColumnName("cm_mobile");
      entity.Property(e => e.CmRef1)
              .HasMaxLength(255)
              .HasColumnName("cm_ref1");
      entity.Property(e => e.CmRef2)
              .HasMaxLength(255)
              .HasColumnName("cm_ref2");
      entity.Property(e => e.CmWork)
              .HasMaxLength(255)
              .HasColumnName("cm_work");
      entity.Property(e => e.CmWorkaddress).HasColumnName("cm_workaddress");
      entity.Property(e => e.ContactPersonOrDeptt)
              .HasColumnType("character varying")
              .HasColumnName("ContactPersonOrDeptt ");
      entity.Property(e => e.ContractDate).HasColumnType("character varying");
      entity.Property(e => e.Contractamount)
              .HasPrecision(10, 2)
              .HasColumnName("contractamount");
      entity.Property(e => e.CountryIdFk).HasColumnName("country_id_fk");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("character varying")
              .HasColumnName("created_at");
      entity.Property(e => e.CustomerIdFk).HasColumnName("customer_id_fk");
      entity.Property(e => e.Customeracnumber)
              .HasMaxLength(255)
              .HasColumnName("customeracnumber");
      entity.Property(e => e.Cycledate)
              .HasColumnType("character varying")
              .HasColumnName("cycledate");
      entity.Property(e => e.Dpd).HasColumnName("dpd");
      entity.Property(e => e.Dpd06).HasColumnName("dpd0_6");
      entity.Property(e => e.Dpd1318).HasColumnName("dpd13_18");
      entity.Property(e => e.Dpd1924).HasColumnName("dpd19_24");
      entity.Property(e => e.Dpd2536).HasColumnName("dpd25_36");
      entity.Property(e => e.Dpd37plus).HasColumnName("dpd37plus");
      entity.Property(e => e.Dpd712).HasColumnName("dpd7_12");
      entity.Property(e => e.EmailIdFk).HasColumnName("email_id_fk");
      entity.Property(e => e.Faxno)
              .HasColumnType("character varying")
              .HasColumnName("FAXno");
      entity.Property(e => e.FifthlastpaymentDate).HasColumnType("character varying");
      entity.Property(e => e.FourthlastpaymentDate).HasColumnType("character varying");
      entity.Property(e => e.Lastpayment)
              .HasPrecision(10, 2)
              .HasColumnName("lastpayment");
      entity.Property(e => e.Lastpaymentdate)
              .HasColumnType("character varying")
              .HasColumnName("lastpaymentdate");
      entity.Property(e => e.Legalstatus)
              .HasMaxLength(255)
              .HasColumnName("legalstatus");
      entity.Property(e => e.Minimumpayment)
              .HasPrecision(10, 2)
              .HasColumnName("minimumpayment");
      entity.Property(e => e.Openingbalance)
              .HasPrecision(10, 2)
              .HasColumnName("openingbalance");
      entity.Property(e => e.OrganizationIdFk).HasColumnName("organization_id_fk");
      entity.Property(e => e.Outstandingbalance)
              .HasPrecision(10, 2)
              .HasColumnName("outstandingbalance");
      entity.Property(e => e.Overdueamount)
              .HasPrecision(10, 2)
              .HasColumnName("overdueamount");
      entity.Property(e => e.Poboxno)
              .HasMaxLength(255)
              .HasColumnName("poboxno");
      entity.Property(e => e.Principleamount)
              .HasPrecision(10, 2)
              .HasColumnName("principleamount");
      entity.Property(e => e.Product)
              .HasMaxLength(255)
              .HasColumnName("product");
      entity.Property(e => e.ProductIdFk).HasColumnName("product_id_fk");
      entity.Property(e => e.RegionIdFk).HasColumnName("region_id_fk");
      entity.Property(e => e.Remarks).HasColumnName("remarks");
      entity.Property(e => e.Secondlastpaymentdate).HasColumnType("character varying");
      entity.Property(e => e.SegmentIdFk).HasColumnName("segment_id_fk");
      entity.Property(e => e.SixthlastpaymentDate).HasColumnType("character varying");
      entity.Property(e => e.Specialinfo).HasColumnName("specialinfo");
      entity.Property(e => e.Status)
              .HasMaxLength(255)
              .HasColumnName("status");
      entity.Property(e => e.ThirdlastpaymentDate).HasColumnType("character varying");
      entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
      entity.Property(e => e.UserIdFk).HasColumnName("user_id_fk");
      entity.Property(e => e.Writtenoffamount)
              .HasPrecision(10, 2)
              .HasColumnName("writtenoffamount");
      entity.Property(e => e.Writtenoffdate)
              .HasColumnType("character varying")
              .HasColumnName("writtenoffdate");

      entity.HasOne(d => d.CenterIdFkNavigation).WithMany(p => p.Accounts)
              .HasForeignKey(d => d.CenterIdFk)
              .HasConstraintName("fk_center_id_fk");

      entity.HasOne(d => d.CountryIdFkNavigation).WithMany(p => p.Accounts)
              .HasForeignKey(d => d.CountryIdFk)
              .HasConstraintName("fk_country_id_fk");

      entity.HasOne(d => d.CustomerIdFkNavigation).WithMany(p => p.AccountCustomerIdFkNavigations)
              .HasForeignKey(d => d.CustomerIdFk)
              .HasConstraintName("fk_customer_id_fk");

      entity.HasOne(d => d.EmailIdFkNavigation).WithMany(p => p.Accounts)
              .HasForeignKey(d => d.EmailIdFk)
              .HasConstraintName("fk_email_id_fk");

      entity.HasOne(d => d.OrganizationIdFkNavigation).WithMany(p => p.Accounts)
              .HasForeignKey(d => d.OrganizationIdFk)
              .HasConstraintName("fk_organization_id_fk");

      entity.HasOne(d => d.ProductIdFkNavigation).WithMany(p => p.Accounts)
              .HasForeignKey(d => d.ProductIdFk)
              .HasConstraintName("fk_product_id_fk");

      entity.HasOne(d => d.RegionIdFkNavigation).WithMany(p => p.Accounts)
              .HasForeignKey(d => d.RegionIdFk)
              .HasConstraintName("fk_region_id_fk");

      entity.HasOne(d => d.SegmentIdFkNavigation).WithMany(p => p.Accounts)
              .HasForeignKey(d => d.SegmentIdFk)
              .HasConstraintName("fk_segment_id_fk");

      entity.HasOne(d => d.UserIdFkNavigation).WithMany(p => p.AccountUserIdFkNavigations)
              .HasForeignKey(d => d.UserIdFk)
              .HasConstraintName("accounts_user_id_fk_fkey");
    });

    modelBuilder.Entity<Actioncode>(entity =>
    {
      entity.HasKey(e => e.ActionId).HasName("actioncode_pkey");

      entity.ToTable("actioncode");

      entity.Property(e => e.ActionId).HasColumnName("action_id");
      entity.Property(e => e.Actioncode1)
              .HasMaxLength(50)
              .HasColumnName("actioncode");
      entity.Property(e => e.Adescription).HasColumnName("adescription");
      entity.Property(e => e.Amount)
              .HasDefaultValue(0)
              .HasColumnName("amount");
      entity.Property(e => e.Aname)
              .HasMaxLength(100)
              .HasColumnName("aname");
      entity.Property(e => e.Color)
              .HasMaxLength(10)
              .HasDefaultValueSql("'#ffffff'::character varying")
              .HasColumnName("color");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("time with time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.FollowUpDate)
              .HasDefaultValue(0)
              .HasColumnName("followUpDate");
      entity.Property(e => e.UpdatedAt)
              .HasColumnType("time with time zone")
              .HasColumnName("updated_at");
    });

    modelBuilder.Entity<Campaign>(entity =>
    {
      entity.HasKey(e => e.CampaignId).HasName("campaigns_pkey");

      entity.ToTable("campaigns");

      entity.Property(e => e.CampaignId).HasColumnName("campaign_id");
      entity.Property(e => e.CampaignDesc)
              .HasMaxLength(200)
              .HasColumnName("campaign_desc");
      entity.Property(e => e.CampaignName)
              .HasMaxLength(150)
              .HasColumnName("campaign_name");
      entity.Property(e => e.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP")
              .HasColumnName("created_at");
      entity.Property(e => e.Status)
              .HasDefaultValue(1)
              .HasColumnName("status");
      entity.Property(e => e.UpdatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP")
              .HasColumnName("updated_at");
    });

    modelBuilder.Entity<Campaignslist>(entity =>
    {
      entity.HasKey(e => e.CampaignslistsId).HasName("campaignslists_pkey");

      entity.ToTable("campaignslists");

      entity.Property(e => e.CampaignslistsId).HasColumnName("campaignslists_id");
      entity.Property(e => e.CampaignIdFk).HasColumnName("campaign_id_fk");
      entity.Property(e => e.CampaignslistsName)
              .HasMaxLength(250)
              .HasColumnName("campaignslists_name");
      entity.Property(e => e.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP")
              .HasColumnName("created_at");
      entity.Property(e => e.Status)
              .HasDefaultValue(1)
              .HasColumnName("status");
      entity.Property(e => e.Target)
              .HasDefaultValue(0)
              .HasColumnName("target");
      entity.Property(e => e.UpdatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP")
              .HasColumnName("updated_at");
      entity.Property(e => e.UserIdFk).HasColumnName("user_id_fk");

      entity.HasOne(d => d.CampaignIdFkNavigation).WithMany(p => p.Campaignslists)
              .HasForeignKey(d => d.CampaignIdFk)
              .HasConstraintName("campaignslists_campaign_id_fk_fkey");

      entity.HasOne(d => d.UserIdFkNavigation).WithMany(p => p.Campaignslists)
              .HasForeignKey(d => d.UserIdFk)
              .HasConstraintName("fk_user_id_fk");
    });

    modelBuilder.Entity<Campaignslistuser>(entity =>
    {
      entity.HasKey(e => e.CampaignslistusersId).HasName("campaignslistusers_pkey");

      entity.ToTable("campaignslistusers");

      entity.Property(e => e.CampaignslistusersId).HasColumnName("campaignslistusers_id");
      entity.Property(e => e.CampaignslistsIdFk).HasColumnName("campaignslists_id_fk");
      entity.Property(e => e.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP")
              .HasColumnName("created_at");
      entity.Property(e => e.Status)
              .HasDefaultValue(1)
              .HasColumnName("status");
      entity.Property(e => e.UpdatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP")
              .HasColumnName("updated_at");
      entity.Property(e => e.UserIdFk).HasColumnName("user_id_fk");

      entity.HasOne(d => d.CampaignslistsIdFkNavigation).WithMany(p => p.Campaignslistusers)
              .HasForeignKey(d => d.CampaignslistsIdFk)
              .HasConstraintName("campaignslistusers_campaignslists_id_fk_fkey");

      entity.HasOne(d => d.UserIdFkNavigation).WithMany(p => p.Campaignslistusers)
              .HasForeignKey(d => d.UserIdFk)
              .HasConstraintName("campaignslistusers_user_id_fk_fkey");
    });

    modelBuilder.Entity<Center>(entity =>
    {
      entity.HasKey(e => e.CenterId).HasName("center_pkey");

      entity.ToTable("center");

      entity.Property(e => e.CenterId).HasColumnName("center_id");
      entity.Property(e => e.Cname)
              .HasMaxLength(255)
              .HasColumnName("cname");
    });

    modelBuilder.Entity<Country>(entity =>
    {
      entity.HasKey(e => e.Countryid).HasName("countries_pkey");

      entity.ToTable("countries");

      entity.Property(e => e.Countryid).HasColumnName("countryid");
      entity.Property(e => e.Countryname)
              .HasMaxLength(255)
              .HasColumnName("countryname");
    });

    modelBuilder.Entity<Email>(entity =>
    {
      entity.HasKey(e => e.EmailId).HasName("email_pkey");

      entity.ToTable("email");

      entity.Property(e => e.EmailId).HasColumnName("email_id");
      entity.Property(e => e.CreatedAt).HasColumnName("created_at");
      entity.Property(e => e.Email1)
              .HasMaxLength(100)
              .HasColumnName("email");
      entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
    });

    modelBuilder.Entity<Emailacc>(entity =>
    {
      entity.HasKey(e => e.EmailAccId).HasName("emailacc_pkey");

      entity.ToTable("emailacc");

      entity.Property(e => e.EmailAccId).HasColumnName("email_acc_id");
      entity.Property(e => e.AccountIdFk).HasColumnName("account_id_fk");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("time with time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.EmailtextIdFk).HasColumnName("emailtext_id_fk");

      entity.HasOne(d => d.AccountIdFkNavigation).WithMany(p => p.Emailaccs)
              .HasForeignKey(d => d.AccountIdFk)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_account_id_fk");

      entity.HasOne(d => d.EmailtextIdFkNavigation).WithMany(p => p.Emailaccs)
              .HasForeignKey(d => d.EmailtextIdFk)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_emailtext_id_fk");
    });

    modelBuilder.Entity<Emailtext>(entity =>
    {
      entity.HasKey(e => e.EmailTextId).HasName("emailtext_pkey");

      entity.ToTable("emailtext");

      entity.Property(e => e.EmailTextId).HasColumnName("email_text_id");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("time with time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.Email).HasColumnName("email");
      entity.Property(e => e.Subject)
              .HasMaxLength(100)
              .HasColumnName("subject");
    });

    modelBuilder.Entity<Nationality>(entity =>
    {
      entity.HasKey(e => e.NationalityId).HasName("nationality_pkey");

      entity.ToTable("nationality");

      entity.Property(e => e.NationalityId).HasColumnName("nationality_id");
      entity.Property(e => e.CreatedAt).HasColumnName("created_at");
      entity.Property(e => e.Nationality1)
              .HasMaxLength(70)
              .HasColumnName("nationality");
      entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
      entity.Property(e => e.UserIdFk).HasColumnName("user_id_fk");

      entity.HasOne(d => d.UserIdFkNavigation).WithMany(p => p.Nationalities)
              .HasForeignKey(d => d.UserIdFk)
              .HasConstraintName("nationality_user_id_fk_fkey");
    });

    modelBuilder.Entity<Note>(entity =>
    {
      entity.HasKey(e => e.NoteId).HasName("notes_pkey");

      entity.ToTable("notes");

      entity.Property(e => e.NoteId).HasColumnName("note_id");
      entity.Property(e => e.AccountIdFk).HasColumnName("account_id_fk");
      entity.Property(e => e.CompanySearch)
              .HasMaxLength(150)
              .HasColumnName("company_search");
      entity.Property(e => e.CreatedAt).HasColumnName("created_at");
      entity.Property(e => e.EmailSearchOrBobox)
              .HasMaxLength(150)
              .HasColumnName("email_search_or_bobox");
      entity.Property(e => e.EmiratesId)
              .HasMaxLength(150)
              .HasColumnName("emirates_id");
      entity.Property(e => e.Feedback).HasColumnName("feedback");
      entity.Property(e => e.ImmigrationChk)
              .HasMaxLength(150)
              .HasColumnName("immigration_chk");
      entity.Property(e => e.NewContactOrAddress)
              .HasMaxLength(150)
              .HasColumnName("new_contact_or_address");
      entity.Property(e => e.SocOrRecWeb)
              .HasMaxLength(150)
              .HasColumnName("soc_or_rec_web");
      entity.Property(e => e.SpecialInfo).HasColumnName("special_info");
      entity.Property(e => e.Status)
              .HasDefaultValue(1)
              .HasColumnName("status");
      entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

      entity.HasOne(d => d.AccountIdFkNavigation).WithMany(p => p.Notes)
              .HasForeignKey(d => d.AccountIdFk)
              .HasConstraintName("fk_account_id_fk");
    });

    modelBuilder.Entity<Organization>(entity =>
    {
      entity.HasKey(e => e.Organizationid).HasName("organizations_pkey");

      entity.ToTable("organizations");

      entity.Property(e => e.Organizationid).HasColumnName("organizationid");
      entity.Property(e => e.Organizationname)
              .HasMaxLength(255)
              .HasColumnName("organizationname");
    });

    modelBuilder.Entity<Payment>(entity =>
    {
      entity.HasKey(e => e.PaymentId).HasName("payments_pkey");

      entity.ToTable("payments");

      entity.Property(e => e.PaymentId).HasColumnName("payment_id");
      entity.Property(e => e.AccountIdFk).HasColumnName("account_id_fk");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("character varying")
              .HasColumnName("created_at");
      entity.Property(e => e.Payment1).HasColumnName("payment");
      entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

      entity.HasOne(d => d.AccountIdFkNavigation).WithMany(p => p.Payments)
              .HasForeignKey(d => d.AccountIdFk)
              .HasConstraintName("payments_account_id_fk_fkey");
    });

    modelBuilder.Entity<Product>(entity =>
    {
      entity.HasKey(e => e.ProductId).HasName("product_pkey");

      entity.ToTable("product");

      entity.Property(e => e.ProductId).HasColumnName("product_id");
      entity.Property(e => e.Pname)
              .HasMaxLength(255)
              .HasColumnName("pname");
    });

    modelBuilder.Entity<Region>(entity =>
    {
      entity.HasKey(e => e.Regionid).HasName("regions_pkey");

      entity.ToTable("regions");

      entity.Property(e => e.Regionid).HasColumnName("regionid");
      entity.Property(e => e.Regionname)
              .HasMaxLength(255)
              .HasColumnName("regionname");
    });

    modelBuilder.Entity<Remark>(entity =>
    {
      entity.HasKey(e => e.RemarkId).HasName("remarks_pkey");

      entity.ToTable("remarks");

      entity.Property(e => e.RemarkId).HasColumnName("remark_id");
      entity.Property(e => e.AccountIdFk).HasColumnName("account_id_fk");
      entity.Property(e => e.ActioncodeIdFk).HasColumnName("actioncode_id_fk");
      entity.Property(e => e.CreatedAt).HasColumnName("created_at");
      entity.Property(e => e.FollowUpDate)
              .HasColumnType("character varying")
              .HasColumnName("follow_up_date");
      entity.Property(e => e.PtpAmount).HasColumnName("PTP_Amount");
      entity.Property(e => e.PtpDate)
              .HasColumnType("character varying")
              .HasColumnName("PTP_Date");
      entity.Property(e => e.RemarkText).HasColumnName("remark_text");
      entity.Property(e => e.StatuscodeIdFk).HasColumnName("statuscode_id_fk");
      entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
      entity.Property(e => e.UserIdFk).HasColumnName("user_id_fk");

      entity.HasOne(d => d.AccountIdFkNavigation).WithMany(p => p.RemarksNavigation)
              .HasForeignKey(d => d.AccountIdFk)
              .HasConstraintName("fk_account_id_fk");

      entity.HasOne(d => d.ActioncodeIdFkNavigation).WithMany(p => p.Remarks)
              .HasForeignKey(d => d.ActioncodeIdFk)
              .HasConstraintName("fk_actioncode_id_fk");

      entity.HasOne(d => d.StatuscodeIdFkNavigation).WithMany(p => p.Remarks)
              .HasForeignKey(d => d.StatuscodeIdFk)
              .HasConstraintName("fk_statuscode_id_fk");

      entity.HasOne(d => d.UserIdFkNavigation).WithMany(p => p.Remarks)
              .HasForeignKey(d => d.UserIdFk)
              .HasConstraintName("fk_user_id_fk");
    });

    modelBuilder.Entity<Role>(entity =>
    {
      entity.HasKey(e => e.RoleId).HasName("roles_pkey");

      entity.ToTable("roles");

      entity.Property(e => e.RoleId).HasColumnName("role_id");
      entity.Property(e => e.CreatedAt).HasColumnName("created_at");
      entity.Property(e => e.Permissions)
              .HasMaxLength(100)
              .HasColumnName("permissions");
      entity.Property(e => e.RoleName)
              .HasMaxLength(50)
              .HasColumnName("role_name");
      entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
    });

    modelBuilder.Entity<Segment>(entity =>
    {
      entity.HasKey(e => e.Segmentid).HasName("segments_pkey");

      entity.ToTable("segments");

      entity.Property(e => e.Segmentid).HasColumnName("segmentid");
      entity.Property(e => e.Segmentname)
              .HasMaxLength(255)
              .HasColumnName("segmentname");
    });

    modelBuilder.Entity<Smsacc>(entity =>
    {
      entity.HasKey(e => e.SmsAccId).HasName("smsacc_pkey");

      entity.ToTable("smsacc");

      entity.Property(e => e.SmsAccId).HasColumnName("sms_acc_id");
      entity.Property(e => e.AccountIdFk).HasColumnName("account_id_fk");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("time with time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.SmsIdFk).HasColumnName("sms_id_fk");

      entity.HasOne(d => d.AccountIdFkNavigation).WithMany(p => p.Smsaccs)
              .HasForeignKey(d => d.AccountIdFk)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_account_id_fk");

      entity.HasOne(d => d.SmsIdFkNavigation).WithMany(p => p.Smsaccs)
              .HasForeignKey(d => d.SmsIdFk)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("fk_sms_id_fk");
    });

    modelBuilder.Entity<Smstext>(entity =>
    {
      entity.HasKey(e => e.SmsTextId).HasName("smstext_pkey");

      entity.ToTable("smstext");

      entity.Property(e => e.SmsTextId).HasColumnName("sms_text_id");
      entity.Property(e => e.CreatedAt).HasColumnName("created_at");
      entity.Property(e => e.Sms).HasColumnName("sms");
      entity.Property(e => e.Subject)
              .HasMaxLength(100)
              .HasColumnName("subject");
    });

    modelBuilder.Entity<Statuscode>(entity =>
    {
      entity.HasKey(e => e.StatusId).HasName("statuscode_pkey");

      entity.ToTable("statuscode");

      entity.Property(e => e.StatusId).HasColumnName("status_id");
      entity.Property(e => e.Amount)
              .HasDefaultValue(0)
              .HasColumnName("amount");
      entity.Property(e => e.Color)
              .HasMaxLength(10)
              .HasDefaultValueSql("'#ffffff'::character varying")
              .HasColumnName("color");
      entity.Property(e => e.CreatedAt)
              .HasColumnType("time with time zone")
              .HasColumnName("created_at");
      entity.Property(e => e.FollowupDate)
              .HasDefaultValue(0)
              .HasColumnName("followupDate");
      entity.Property(e => e.Sdescription).HasColumnName("sdescription");
      entity.Property(e => e.Sname)
              .HasMaxLength(100)
              .HasColumnName("sname");
      entity.Property(e => e.Statuscode1)
              .HasMaxLength(50)
              .HasColumnName("statuscode");
      entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
    });

    modelBuilder.Entity<User>(entity =>
    {
      entity.HasKey(e => e.UserId).HasName("users_pkey");

      entity.ToTable("users");

      entity.Property(e => e.UserId).HasColumnName("user_id");
      entity.Property(e => e.Cnic)
              .HasMaxLength(50)
              .HasColumnName("cnic");
      entity.Property(e => e.Cnicexpiry)
              .HasColumnType("character varying")
              .HasColumnName("cnicexpiry");
      entity.Property(e => e.CreatedAt).HasColumnName("created_at");
      entity.Property(e => e.CreatedbyIdFk).HasColumnName("createdby_id_fk");
      entity.Property(e => e.CreatedtoIdFk).HasColumnName("createdto_id_fk");
      entity.Property(e => e.Dob)
              .HasMaxLength(50)
              .HasColumnName("dob");
      entity.Property(e => e.Gender)
              .HasMaxLength(30)
              .HasColumnName("gender");
      entity.Property(e => e.Passport)
              .HasMaxLength(50)
              .HasColumnName("passport");
      entity.Property(e => e.Passportexpiry)
              .HasColumnType("character varying")
              .HasColumnName("passportexpiry");
      entity.Property(e => e.Password)
              .HasColumnType("character varying")
              .HasColumnName("password");
      entity.Property(e => e.RoleIdFk).HasColumnName("role_id_fk");
      entity.Property(e => e.Status)
              .HasDefaultValue(1)
              .HasColumnName("status");
      entity.Property(e => e.Uname)
              .HasMaxLength(100)
              .HasColumnName("uname");
      entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
      entity.Property(e => e.UserName)
              .HasMaxLength(50)
              .HasColumnName("user_name");

      entity.HasOne(d => d.CreatedbyIdFkNavigation).WithMany(p => p.InverseCreatedbyIdFkNavigation)
              .HasForeignKey(d => d.CreatedbyIdFk)
              .HasConstraintName("fk_createdby_id_fk");

      entity.HasOne(d => d.CreatedtoIdFkNavigation).WithMany(p => p.InverseCreatedtoIdFkNavigation)
              .HasForeignKey(d => d.CreatedtoIdFk)
              .HasConstraintName("fk_createdto_id_fk");

      entity.HasOne(d => d.RoleIdFkNavigation).WithMany(p => p.Users)
              .HasForeignKey(d => d.RoleIdFk)
              .HasConstraintName("users_role_id_fk_fkey");
    });

    OnModelCreatingPartial(modelBuilder);
  }

  partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
