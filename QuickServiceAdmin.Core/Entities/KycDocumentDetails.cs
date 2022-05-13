using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("KYC_DOCUMENT_DETAILS")]
    public class KycDocumentDetails
    {
        public KycDocumentDetails()
        {
            KycDocumentDocs = new HashSet<KycDocumentDocs>();
        }

        [JsonIgnore] [Key] [Column("ID")] public long Id { get; set; }
        [Column("PHONE_NUMBER")] public string PhoneNumber { get; set; }
        [Column("EMAIL_ADDRESS")] public string EmailAddress { get; set; }
        [Column("ACCOUNT_SEGMENT")] public string AccountSegment { get; set; }
        [Column("ADDITIONAL_COMMENTS")] public string AdditionalComments { get; set; }

        [JsonIgnore] public long CustomerRequestId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CustomerRequestId))]
        [InverseProperty("KycDocumentDetails")]
        public virtual CustomerRequest CustomerRequest { get; set; }

        [InverseProperty("KycDocumentDetail")] public virtual ICollection<KycDocumentDocs> KycDocumentDocs { get; set; }
    }
}