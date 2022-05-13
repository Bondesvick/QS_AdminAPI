using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("INTERNET_BANKING_ONBOARDING_DETAILS")]
    public class InternetBankingOnboardingDetails
    {
        [Key] [JsonIgnore] [Column("ID")] public long Id { get; set; }

        [Column("PHONE_NUMBER")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }


        [Column("ACCESS_TYPE")]
        [StringLength(50)]
        public string AccessType { get; set; }

        [Column("HAVE_DEBIT_CREDIT")]
        [StringLength(10)]
        public string HaveDebitCredit { get; set; }

        [Column("CUSTOMER_REQ_ID")]
        [JsonIgnore]
        public long CustomerReqId { get; set; }

        [ForeignKey(nameof(CustomerReqId))]
        [InverseProperty(nameof(CustomerRequest.InternetBankingOnboardingDetails))]
        [JsonIgnore]
        public virtual CustomerRequest CustomerReq { get; set; }
    }
}