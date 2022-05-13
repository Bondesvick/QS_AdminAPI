using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("CUSTOMER_REQUEST")]
    public class CustomerRequest
    {
        public CustomerRequest()
        {
            AccountUpgradeCifDetails = new HashSet<AccountUpgradeCifDetails>();
            AccountUpgradeDetails = new HashSet<AccountUpgradeDetails>();
            AccountUpgradeDoc = new HashSet<AccountUpgradeDoc>();
            AddAccOpeningDetails = new HashSet<AddAccOpeningDetails>();
            CustomerRequestDocuments = new HashSet<CustomerRequestDocuments>();
            DataUpdateDetails = new HashSet<DataUpdateDetails>();
            FailedTransactionDetails = new HashSet<FailedTransactionDetails>();
            FailedTransactionDoc = new HashSet<FailedTransactionDoc>();
            SmeOnboardingDetails = new HashSet<SmeOnboardingDetails>();
            PasswordResetDoc = new HashSet<PasswordResetDoc>();
            AcctReactivationDetails = new HashSet<AcctReactivationDetails>();
            AcctReactivationDoc = new HashSet<AcctReactivationDoc>();
            InternetBankingOnboardingDetails = new HashSet<InternetBankingOnboardingDetails>();
            InternetBankingOnboardingDoc = new HashSet<InternetBankingOnboardingDoc>();
            CorpAccOpeningDetails = new HashSet<CorpAccOpeningDetails>();
            CorpAccOpeningDirectorDetails = new HashSet<CorpAccOpeningDirectorDetails>();
            CorpAccOpeningDoc = new HashSet<CorpAccOpeningDoc>();
            CorpAccOpeningEnterOnline = new HashSet<CorpAccOpeningEnterOnline>();
            CorpAccOpeningShareHolder = new HashSet<CorpAccOpeningShareHolder>();
            CorpAccOpeningSignatory = new HashSet<CorpAccOpeningSignatory>();
            LoanRequestDetails = new HashSet<LoanRequestDetails>();
            LoanRequestDoc = new HashSet<LoanRequestDoc>();
            //LoanRepaymentDetails = new HashSet<LoanRepaymentDetails>();
            LoanRepaymentDocuments = new HashSet<LoanRepaymentDocument>();
            DebitCardDetails = new HashSet<DebitCardDetails>();
        }

        [Key] [Column("ID")] public long Id { get; set; }
        [Column("TRAN_ID")] [StringLength(20)] public string TranId { get; set; }

        [Column("ACCOUNT_NUMBER")]
        [StringLength(10)]
        public string AccountNumber { get; set; }

        [Column("ACCOUNT_NAME")]
        [StringLength(500)]
        public string AccountName { get; set; }

        [Required]
        [Column("CUSTOMER_AUTH_TYPE")]
        [StringLength(20)]
        public string CustomerAuthType { get; set; }

        [Required]
        [Column("STATUS")]
        [StringLength(50)]
        public string Status { get; set; }

        [Column("CREATED_DATE")] public DateTime CreatedDate { get; set; }

        [Column("TREATED_BY")]
        [StringLength(20)]
        public string TreatedBy { get; set; }

        [Column("TREATED_DATE")] public DateTime? TreatedDate { get; set; }

        [Required]
        [Column("REQUEST_TYPE")]
        [StringLength(200)]
        public string RequestType { get; set; }

        [Column("TREATED_BY_UNIT")]
        [StringLength(100)]
        public string TreatedByUnit { get; set; }

        [Column("REJECTION_REASON")]
        [StringLength(300)]
        public string RejectionReason { get; set; }

        [Column("REMARKS")]
        [StringLength(500)]
        public string Remarks { get; set; }

        [Column("BVN")] [StringLength(11)] public string Bvn { get; set; }
        public string AssignedBy { get; set; }
        public string AssignedTo { get; set; }

        [InverseProperty("CustomerRequest")]
        [JsonIgnore]
        public virtual KycDocumentDetails KycDocumentDetails { get; set; }

        [InverseProperty("CustomerRequest")]
        [JsonIgnore]
        public virtual RequestAndStatusMgtDetails RequestAndStatusMgtDetails { get; set; }

        [InverseProperty("CustomerRequest")]
        [JsonIgnore]
        public virtual CardRequestDetails CardRequestDetails { get; set; }

        [InverseProperty("CustomerRequest")]
        [JsonIgnore]
        public virtual AddressRequestDetails AddressRequestDetails { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<AccountUpgradeCifDetails> AccountUpgradeCifDetails { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<AccountUpgradeDetails> AccountUpgradeDetails { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<AccountUpgradeDoc> AccountUpgradeDoc { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<AddAccOpeningDetails> AddAccOpeningDetails { get; set; }

        [InverseProperty("CustomerRequest")]
        [JsonIgnore]
        public virtual ICollection<CustomerRequestDocuments> CustomerRequestDocuments { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<DataUpdateDetails> DataUpdateDetails { get; set; }

        [InverseProperty("CustomerRequest")]
        [JsonIgnore]
        public virtual ICollection<FailedTransactionDetails> FailedTransactionDetails { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<FailedTransactionDoc> FailedTransactionDoc { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<SmeOnboardingDetails> SmeOnboardingDetails { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<PasswordResetDoc> PasswordResetDoc { get; set; }

        [InverseProperty("CustomerRequest")]
        [JsonIgnore]
        public virtual ICollection<AcctReactivationDetails> AcctReactivationDetails { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<AcctReactivationDoc> AcctReactivationDoc { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<InternetBankingOnboardingDetails> InternetBankingOnboardingDetails { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<InternetBankingOnboardingDoc> InternetBankingOnboardingDoc { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<CorpAccOpeningDetails> CorpAccOpeningDetails { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<CorpAccOpeningDirectorDetails> CorpAccOpeningDirectorDetails { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<CorpAccOpeningDoc> CorpAccOpeningDoc { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<CorpAccOpeningEnterOnline> CorpAccOpeningEnterOnline { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<CorpAccOpeningShareHolder> CorpAccOpeningShareHolder { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<CorpAccOpeningSignatory> CorpAccOpeningSignatory { get; set; }

        [InverseProperty("CustomerRequest")]
        [JsonIgnore]
        public virtual FacialIdentityRequestDetails FacialIdentityRequestDetails { get; set; }

        [InverseProperty("CustomerRequest")]
        [JsonIgnore]
        public virtual ICollection<LoanRequestDetails> LoanRequestDetails { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<LoanRequestDoc> LoanRequestDoc { get; set; }

        [InverseProperty("CustomerRequest")]
        [JsonIgnore]
        public virtual LoanRepaymentDetails LoanRepaymentDetails { get; set; }

        [InverseProperty("CustomerRequest")]
        [JsonIgnore]
        public virtual ICollection<LoanRepaymentDocument> LoanRepaymentDocuments { get; set; }

        [InverseProperty("CustomerReq")]
        [JsonIgnore]
        public virtual ICollection<DebitCardDetails> DebitCardDetails { get; set; }
    }
}