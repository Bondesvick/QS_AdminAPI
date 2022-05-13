using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("SME_ONBOARDING_DETAILS")]
    public class SmeOnboardingDetails
    {
        public SmeOnboardingDetails()
        {
            SmeOnboardingDocs = new HashSet<SmeOnboardingDocs>();
        }

        [JsonIgnore] [Key] [Column("ID")] public int Id { get; set; }

        [Column("CUSTOMER_REQ_ID")]
        [JsonIgnore]
        public long CustomerReqId { get; set; }


        [Column("PHONE_NUMBER")]
        [StringLength(200)]
        public string PhoneNumber { get; set; }

        [ForeignKey(nameof(CustomerReqId))]
        [InverseProperty(nameof(CustomerRequest.SmeOnboardingDetails))]
        [JsonIgnore]
        public virtual CustomerRequest CustomerReq { get; set; }

        [InverseProperty("Req")] public virtual ICollection<SmeOnboardingDocs> SmeOnboardingDocs { get; set; }
    }
}