using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("DEBIT_CARD_DETAILS")]
    public partial class DebitCardDetails
    {
        public DebitCardDetails()
        {
            //Documents ??= new List<DebitCardDocument>();
        }

        [Key] [Column("ID")] [JsonIgnore] public int Id { get; set; }
        [Column("CUSTOMER_REQ_ID")] public long CustomerReqId { get; set; }

        [ForeignKey(nameof(CustomerReqId))]
        [InverseProperty("DebitCardDetails")]
        [JsonIgnore]
        public virtual CustomerRequest CustomerReq { get; set; }

        //[InverseProperty("AccountOpeningRequest")]
        //[JsonIgnore]
        //public virtual ICollection<DebitCardDocument> Documents { get; set; }

        // Account Info
        [Column("ACCOUNT_STATUS")] public string AccountStatus { get; set; }

        public string AuthType { get; set; }
        [Column("BVN")] public string BVN { get; set; }
        [Column("PHONE_NUMBER")] public string PhoneNumber { get; set; }

        // Data

        [Column("DATE_OF_BIRTH")] public string DateOfBirth { get; set; }
        [Column("REQUEST_TYPE")] public string RequestType { get; set; }
        [Column("NAME_ON_CARD")] public string NameOnCard { get; set; }
        [Column("BRANCH")] public string Branch { get; set; }
        [Column("ACCOUNT_TO_DEBIT")] public string AccountToDebit { get; set; }
        [Column("HOTLISTED_CARD")] public string hotlistedCard { get; set; }
        [Column("HOTLIST_CODE")] public string hotlistCode { get; set; }
        public string CardType { get; set; }

        // Session
        [Column("CASE_ID")] public string CaseId { get; set; }

        [Column("CURRENT_STEP")] public string CurrentStep { get; set; }
        [Column("SUBMITTED")] public bool Submitted { get; set; }

        // Terms and Conditions
        [Column("I_ACCEPT_TERMS_AND_CONDITIONS")] public bool IAcceptTermsAndCondition { get; set; }

        [Column("DATE_OF_ACCEPTING_T_AND_C")] public DateTime DateOfAcceptingTAndC { get; set; }
    }
}