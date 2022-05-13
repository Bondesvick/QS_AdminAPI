using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("CARD_REQUEST_DETAILS")]
    public class CardRequestDetails
    {

        [JsonIgnore] [Key] [Column("ID")]
        public long Id { get; set; }

        [Column("STATUS")] [Required] public string Status { get; set; } = "IDLE";

        [Column("COMMENT")] public string Comment { get; set; }

        [Column("ACCOUNT_NUMBER")]
        [Required] public string AccountNumber { get; set; }

        [Column("CITY")]
        [Required] public string City { get; set; }

        [Column("CUSTOMER_TITLE")]
        [Required] public string CustomerTitle { get; set; }

        [Column("MARITAL_STATUS")]
        [Required] public string MaritalStatus { get; set; }

        [Column("CUSTOMER_GENDER")]
        [Required] public string CustomerGender { get; set; }

        [Column("INITIATING_BRANCH")]
        [Required] public string InitiatingBranch { get; set; }

        [Column("COLLECTION_BRANCH")]
        [Required] public string CollectionBranch { get; set; }

        [Column("CARD_TYPE")]
        [Required] public string CardType { get; set; }

        [Column("PREFERRED_NAME_ON_CARD")]
        [Required] public string PreferredNameOnCard { get; set; }

        [Column("ACCOUNT_TO_DEBIT")]
        [Required] public string AccountToDebit { get; set; }

        [Column("INITIATED_BY")]
        [Required] public string InitiatedBy { get; set; }

        [Column("APPROVED_BY")]
        public string ApprovedBy { get; set; }

        [Column("CUSTOMER_REQUEST_ID")]
        [Required]
        [JsonIgnore] public long CustomerRequestId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CustomerRequestId))]
        [InverseProperty("CardRequestDetails")]
        public virtual CustomerRequest CustomerRequest { get; set; }
    }
}
