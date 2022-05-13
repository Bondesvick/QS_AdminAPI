using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace QuickServiceAdmin.Core.Entities
{
    public partial class QuickServiceContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public QuickServiceContext(DbContextOptions<QuickServiceContext> options, IConfiguration configuration)
            : base(options) => _configuration = configuration;

        public virtual DbSet<AccountUpgradeCifDetails> AccountUpgradeCifDetails { get; set; }
        public virtual DbSet<AccountUpgradeDetails> AccountUpgradeDetails { get; set; }
        public virtual DbSet<AccountUpgradeDoc> AccountUpgradeDoc { get; set; }
        public virtual DbSet<AddAccOpeningDetails> AddAccOpeningDetails { get; set; }
        public virtual DbSet<AddAccOpeningDocs> AddAccOpeningDocs { get; set; }
        public virtual DbSet<CustomerRequest> CustomerRequest { get; set; }
        public virtual DbSet<CustomerRequestDocuments> CustomerRequestDocuments { get; set; }
        public virtual DbSet<DataUpdateDetails> DataUpdateDetails { get; set; }
        public virtual DbSet<DataUpdateDocs> DataUpdateDocs { get; set; }
        public virtual DbSet<KycDocumentDetails> KycDocumentDetails { get; set; }
        public virtual DbSet<KycDocumentDocs> KycDocumentDocs { get; set; }

        public virtual DbSet<CardRequestDetails> CardRequestDetails { get; set; }
        // public virtual DbSet<FacialIdentityRequestDetails> FacialIdentityRequestDetails { get; set; }

        public virtual DbSet<FailedTransactionDetails> FailedTransactionDetails { get; set; }
        public virtual DbSet<FailedTransactionDoc> FailedTransactionDoc { get; set; }

        public virtual DbSet<SmeOnboardingDetails> SmeOnboardingDetails { get; set; }
        public virtual DbSet<SmeOnboardingDocs> SmeOnboardingDocs { get; set; }

        public virtual DbSet<PasswordResetDoc> PasswordResetDoc { get; set; }

        public virtual DbSet<AcctReactivationDetails> AcctReactivationDetails { get; set; }
        public virtual DbSet<AcctReactivationDoc> AcctReactivationDoc { get; set; }

        public virtual DbSet<InternetBankingOnboardingDetails> InternetBankingOnboardingDetails { get; set; }
        public virtual DbSet<InternetBankingOnboardingDoc> InternetBankingOnboardingDoc { get; set; }

        public virtual DbSet<CorpAccOpeningDetails> CorpAccOpeningDetails { get; set; }
        public virtual DbSet<CorpAccOpeningDirectorDetails> CorpAccOpeningDirectorDetails { get; set; }
        public virtual DbSet<CorpAccOpeningDoc> CorpAccOpeningDoc { get; set; }

        public virtual DbSet<CityState> CityState { get; set; }
        public virtual DbSet<CorpAccOpeningEnterOnline> CorpAccOpeningEnterOnline { get; set; }
        public virtual DbSet<CorpAccOpeningShareHolder> CorpAccOpeningShareHolder { get; set; }
        public virtual DbSet<CorpAccOpeningSignatory> CorpAccOpeningSignatory { get; set; }
        public virtual DbSet<RequestAndStatusMgtDetails> RequestAndStatusMgtDetails { get; set; }
        public virtual DbSet<RequestAndStatusMgtDocs> RequestAndStatusMgtDocs { get; set; }

        public virtual DbSet<LoanProductDocument> LoanProductDocument { get; set; }
        public virtual DbSet<LoanRequestDetails> LoanRequestDetails { get; set; }
        public virtual DbSet<LoanRequestDoc> LoanRequestDoc { get; set; }
        public virtual DbSet<LoanRequestPlppType> LoanRequestPlppType { get; set; }

        public virtual DbSet<LoanRepaymentDetails> LoanRepaymentDetails { get; set; }
        public virtual DbSet<LoanRepaymentDocument> LoanRepaymentDocuments { get; set; }

        public virtual DbSet<DebitCardDetails> DebitCardDetails { get; set; }
        public virtual DbSet<DebitCardDocument> DebitCardDocuments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseLazyLoadingProxies()
                    .ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning))
                    .UseSqlServer(_configuration.GetConnectionString("QuickServiceDBConnection"));
            //CoreEventId.DetachedLazyLoadingWarning
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerRequest>(entity =>
                       {
                           entity.HasIndex(e => e.Status)
                               .HasName("IX_REQUEST_STATUS");

                           entity.HasIndex(e => e.TreatedByUnit)
                               .HasName("IX_TRBY_UNIT");
                       });

            modelBuilder.Entity<LoanProductDocument>(entity =>
            {
                entity.HasIndex(e => e.IsRequired)
                    .HasName("IX_LOAN_PROD_DOC_IS_REQ");

                entity.HasIndex(e => e.ProductCategory)
                    .HasName("IX_LOAN_PROD_DOC_PROD_CAT");

                entity.HasIndex(e => e.ProductName)
                    .HasName("IX_LOAN_PROD_DOC_PROD_NAME");

                entity.Property(e => e.Note).HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<LoanRequestDetails>(entity =>
            {
                entity.HasIndex(e => e.CustomerRequestId)
                    .HasName("IX_LOAN_REQ_DETAIL_CUST_RQ_ID");

                entity.Property(e => e.AccountName).HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<LoanRequestDoc>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("IX_LOAN_REQ_DOC_CUST_REQ_ID");
            });

            modelBuilder.Entity<LoanRepaymentDetails>(entity =>
            {
                entity.HasOne(t => t.CustomerRequest)
                    .WithOne(t => t.LoanRepaymentDetails)
                    .HasForeignKey<LoanRepaymentDetails>(e => e.CustomerRequestId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.CustomerRequestId)
                    .HasName("IX_LOAN_REPAYMENT_TRANX_DET_CUST_RQ_ID").IsUnique(false);

                //entity.Property(e => e.AccountName).HasDefaultValueSql("(N'')");
            });

            modelBuilder.Entity<LoanRepaymentDocument>(entity =>
            {
                entity.HasOne(t => t.CustomerRequest)
                    .WithMany(t => t.LoanRepaymentDocuments)
                    .HasForeignKey(t => t.CustomerRequestId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => e.CustomerRequestId)
                    .HasName("IX_LOAN_REPAYMENT_DOC_CUST_REQ_ID").IsUnique(false);
            });

            modelBuilder.Entity<DebitCardDetails>(entity =>
            {
                entity.HasOne(d => d.CustomerReq)
                    .WithMany(p => p.DebitCardDetails)
                    .HasForeignKey(d => d.CustomerReqId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DEBIT_CARD_REQUEST_DETAILS");
            });

            //modelBuilder.Entity<DebitCardDocument>(entity =>
            //{
            //    entity.HasOne(d => d.AccountOpeningRequest)
            //        .WithMany(p => p.Documents)
            //        .HasForeignKey(d => d.AccOpeningReqId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_DEBIT_CARD_DOC_DEBIT_CARD_REQ_ID");
            //});

            modelBuilder.Entity<CityState>(entity =>
         {
             entity.Property(e => e.Id).ValueGeneratedNever();

             entity.Property(e => e.City).IsUnicode(false);

             entity.Property(e => e.CityId).IsUnicode(false);

             entity.Property(e => e.Country).IsUnicode(false);

             entity.Property(e => e.CountryCode).IsUnicode(false);

             entity.Property(e => e.CountryCodeDebit).IsUnicode(false);

             entity.Property(e => e.Region).IsUnicode(false);

             entity.Property(e => e.RegionCode).IsUnicode(false);

             entity.Property(e => e.RegionId).IsUnicode(false);
         });

            modelBuilder.Entity<FailedTransactionDetails>(entity =>
            {
                entity.HasIndex(e => e.CustomerRequestId)
                    .HasName("IX_FAILED_TRANX_DET_CUST_RQ_ID");
            });

            modelBuilder.Entity<FailedTransactionDoc>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("IX_FAILED_TRANX_DOC_CUST_REQ_ID");
            });

            modelBuilder.Entity<AccountUpgradeCifDetails>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("ACC_UPGRADE_REQ_ID");
            });

            modelBuilder.Entity<AccountUpgradeDetails>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("ACC_UPGRADE_DET_REQ_ID");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

                entity.Property(e => e.Submitted).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.UndatingAdd).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            });

            modelBuilder.Entity<AccountUpgradeDoc>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("ACC_UPGRADE_DOC_REQ_ID");

                entity.HasIndex(e => e.DocType)
                    .HasName("ACC_UPGRADE_DOC_TYPE");
            });

            modelBuilder.Entity<AddAccOpeningDetails>(entity =>
            {
                entity.HasOne(d => d.CustomerReq)
                    .WithMany(p => p.AddAccOpeningDetails)
                    .HasForeignKey(d => d.CustomerReqId)
                    .HasConstraintName("FK_ADDITIONAL_ACC_OPENING_DETAILS");
            });

            modelBuilder.Entity<AddAccOpeningDocs>(entity =>
            {
                entity.HasOne(d => d.AccOpeningReq)
                    .WithMany(p => p.AddAccOpeningDocs)
                    .HasForeignKey(d => d.AccOpeningReqId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ADD_ACC_OPENING_DOCS_ACC_OPENING_REQ_ID");
            });

            modelBuilder.Entity<CustomerRequest>(entity =>
            {
                entity.HasIndex(e => e.Status)
                    .HasName("IX_REQUEST_STATUS");

                entity.HasIndex(e => e.TreatedByUnit)
                    .HasName("IX_TRBY_UNIT");
            });

            modelBuilder.Entity<CustomerRequestDocuments>(entity => { entity.HasIndex(e => e.CustomerRequestId); });

            modelBuilder.Entity<DataUpdateDetails>(entity =>
            {
                entity.HasOne(d => d.CustomerReq)
                    .WithMany(p => p.DataUpdateDetails)
                    .HasForeignKey(d => d.CustomerReqId)
                    .HasConstraintName("FK_DATA_UPDATE_OPENING_DETAILS");
            });

            modelBuilder.Entity<DataUpdateDocs>(entity =>
            {
                entity.HasOne(d => d.DataUpdateReq)
                    .WithMany(p => p.DataUpdateDocs)
                    .HasForeignKey(d => d.DataUpdateReqId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DATA_UPDATE_DOCS_DATA_UPDATE_REQ_ID");
            });

            modelBuilder.Entity<KycDocumentDetails>(entity =>
            {
                entity.HasIndex(e => e.CustomerRequestId)
                    .IsUnique();
            });

            modelBuilder.Entity<CardRequestDetails>(entity =>
            {
                entity.HasIndex(e => e.CustomerRequestId)
                    .IsUnique();
            });

            // modelBuilder.Entity<FacialIdentityRequestDetails>(entity =>
            // {
            //     entity.HasIndex(e => e.CustomerRequestId)
            //         .IsUnique();
            // });

            modelBuilder.Entity<KycDocumentDocs>(entity => { entity.HasIndex(e => e.KycDocumentDetailId); });

            modelBuilder.Entity<SmeOnboardingDetails>(entity =>
            {
                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.HasOne(d => d.CustomerReq)
                    .WithMany(p => p.SmeOnboardingDetails)
                    .HasForeignKey(d => d.CustomerReqId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SME_ONBOARDING_DETAILS_CUSTOMER_REQUEST");
            });

            modelBuilder.Entity<SmeOnboardingDocs>(entity =>
            {
                entity.HasOne(d => d.Req)
                    .WithMany(p => p.SmeOnboardingDocs)
                    .HasForeignKey(d => d.ReqId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SME_ONBOARDING_DOCS_SME_ONBOARDING_DETAILS");
            });

            modelBuilder.Entity<PasswordResetDoc>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("IX_PASS_RESET_DOC_CUST_REQ_ID");
            });

            modelBuilder.Entity<AcctReactivationDetails>(entity =>
            {
                entity.HasIndex(e => e.CustomerRequestId)
                    .HasName("IX_ACCT_REACTIVATION_DETAIL_CUST_RQ_ID");

                entity.Property(e => e.Income).IsUnicode(false);
            });

            modelBuilder.Entity<AcctReactivationDoc>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("IX_ACCT_REACTIVATION_DOC_CUST_REQ_ID");
            });

            modelBuilder.Entity<InternetBankingOnboardingDetails>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("IX_ONBOARD_DET_REQ_ID");
            });

            modelBuilder.Entity<InternetBankingOnboardingDoc>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("IX_ONBOARD_DOC_REQ_ID");

                entity.HasIndex(e => e.DocType)
                    .HasName("IX_ONBOARD__DOC_TYPE");
            });

            modelBuilder.Entity<CorpAccOpeningDetails>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("IX_CORP_ACC_REQ_ID");
            });

            modelBuilder.Entity<CorpAccOpeningDirectorDetails>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("IX_CORP_ACC_REQ_ID");
            });

            modelBuilder.Entity<CorpAccOpeningDoc>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("IX_CORP_ACC_DOC_REQ_ID");

                entity.HasIndex(e => e.DocType)
                    .HasName("IX_CORP_ACC_DOC_TYPE");
            });

            modelBuilder.Entity<CorpAccOpeningEnterOnline>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("IX_CORP_ACC_REQ_ID");
            });

            modelBuilder.Entity<CorpAccOpeningShareHolder>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("IX_CORP_ACC_DOC_REQ_ID");
            });

            modelBuilder.Entity<CorpAccOpeningSignatory>(entity =>
            {
                entity.HasIndex(e => e.CustomerReqId)
                    .HasName("IX_CORP_ACC_DOC_REQ_ID");
            });

            modelBuilder.Entity<CustomerRequest>(entity =>
            {
                entity.HasIndex(e => e.Status)
                    .HasName("IX_REQUEST_STATUS");

                entity.HasIndex(e => e.TreatedByUnit)
                    .HasName("IX_TRBY_UNIT");
            });

            modelBuilder.Entity<RequestAndStatusMgtDocs>(entity =>
        {
            entity.HasOne(d => d.RequestAndStatusMgt)
                .WithMany(p => p.RequestAndStatusMgtDocs)
                .HasForeignKey(d => d.RequestAndStatusMgtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_REQUEST_AND_STATUS_MGT_DOCS_REQUEST_AND_STATUS_MGT_ID");
        });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}