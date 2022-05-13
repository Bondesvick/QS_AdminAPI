using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("ACCT_REACTIVATION_DETAILS")]
    public class AcctReactivationDetails
    {
        [Key]
        [Column("ID")]
        [JsonIgnore]
        public long Id { get; set; }
        [Column("CUSTOMER_NAME")]
        public string CustomerName { get; set; }

        [Column("ACCOUNT_NUMBER")]
        [StringLength(10)]
        public string AccountNumber { get; set; }

        [Column("PHONE_NUMBER")]
        [StringLength(11)]
        public string PhoneNumber { get; set; }

        [Column("SIGNATORY_BVN")]
        [StringLength(11)]
        public string SignatoryBvn { get; set; }

        [Column("ADDRESS")]
        [StringLength(500)]
        public string Address { get; set; }

        [Column("ADDITIONAL_SERVICES")]
        public string AdditionalServices { get; set; }

        [Column("CUSTOMER_REQUEST_ID")]
        [JsonIgnore]
        public long CustomerRequestId { get; set; }

        [Column("PREFERRED_NAME_ON_CARD")]
        public string PreferredNameOnCard { get; set; }
        [Column("ATM_PICK_UP_BRANCH_ID")]
        public string AtmPickUpBranchId { get; set; }
        [Column("ATM_PICK_UP_BRANCH_NAME")]
        public string AtmPickUpBranchName { get; set; }
        [Column("ATM_PICK_UP_BRANCH_STATE")]
        public string AtmPickUpBranchState { get; set; }

        [Column("PREFERRED_BRANCH")]
        [StringLength(100)]
        public string PreferredBranch { get; set; }

        [Column("NOK_FULLNAME")]
        [StringLength(100)]
        public string NokFullname { get; set; }

        [Column("DOB")]
        [StringLength(50)]
        public string Dob { get; set; }

        [Column("EMAIL_ADDRESS")]
        [StringLength(100)]
        public string EmailAddress { get; set; }

        [Column("GENDER")]
        [StringLength(10)]
        public string Gender { get; set; }

        [Column("EMPLOYEMENT_STATUS")]
        [StringLength(100)]
        public string EmployementStatus { get; set; }

        [Column("OCCUPATION")]
        [StringLength(500)]
        public string Occupation { get; set; }

        [Column("INCOME")]
        [StringLength(500)]
        public string Income { get; set; }

        [ForeignKey(nameof(CustomerRequestId))]
        [InverseProperty("AcctReactivationDetails")]
        [JsonIgnore]
        public virtual CustomerRequest CustomerRequest { get; set; }

        [Column("CURRENT_STEP")]
        public string CurrentStep { get; set; }
        [Column("CASE_ID")]
        [StringLength(50)]
        public string CaseId { get; set; }
        [Column("ACCOUNT_OPTION")]
        public string AccountOption { get; set; }
        [Column("CARDID")]
        [StringLength(50)]
        public string Cardid { get; set; }
        [Column("HOUSENO")]
        [StringLength(50)]
        public string Houseno { get; set; }
        [Column("STREET")]
        [StringLength(50)]
        public string Street { get; set; }
        [Column("CITY")]
        [StringLength(50)]
        public string City { get; set; }
        [Column("STATE")]
        [StringLength(50)]
        public string State { get; set; }
        [Column("LANDMARK")]
        [StringLength(50)]
        public string Landmark { get; set; }
        [Column("ALIAS")]
        [StringLength(50)]
        public string Alias { get; set; }
    }
}