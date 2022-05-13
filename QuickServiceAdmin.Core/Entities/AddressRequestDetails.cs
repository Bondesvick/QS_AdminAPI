using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("ADDRESS_REQUEST_DETAILS")]
    public class AddressRequestDetails
    {

        [JsonIgnore]
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("STATUS")] [Required] public string Status { get; set; } = "IDLE";

        [Column("REMARK")] public string Remark { get; set; }
        [Column("COMMENT")] public string Comment { get; set; }

        [Column("ACCOUNT_NUMBER")] [Required] public string AccountNumber { get; set; }

        [Column("CITY")] [Required] public string City { get; set; }

        [Column("CIF_ID")] [Required] public string CifId { get; set; }

        [Column("ADDRESS_LINE_1")] [Required] public string AddressLine1 { get; set; }


        [Column("ADDRESS_LINE_2")] public string AddressLine2 { get; set; }
        [Column("STATE")] [Required] public string State { get; set; }
        [Column("ALIAS")] [Required] public string Alias { get; set; }
        [Column("CUSTOMER_GENDER")] [Required] public string CustomerGender { get; set; }



        [Column("FIRST_NAME")] [Required] public string FirstName { get; set; }

        [Column("LAST_NAME")] [Required] public string LastName { get; set; }

        [Column("PHONE_NUMBER")] [Required] public string PhoneNumber { get; set; }
        [Column("EMAIL_ADDRESS")] [Required] public string EmailAddress { get; set; }

        [Column("BRANCH_ID")] [Required] public string BranchId { get; set; }

        [Column("LANDMARK")] [Required] public string LandMark { get; set; }
        [Column("COMPANY_NAME")] public string CompanyName { get; set; }

        [Column("INITIATED_BY")] [Required] public string InitiatedBy { get; set; }
        [Column("APPROVED_BY")] public string ApprovedBy { get; set; }

        [Column("REQ_INITIATION_ID")] public string ReqInitiationId { get; set; }


        [Column("CUSTOMER_REQUEST_ID")]
        [Required]
        [JsonIgnore] public long CustomerRequestId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CustomerRequestId))]
        [InverseProperty("AddressRequestDetails")]
        public virtual CustomerRequest CustomerRequest { get; set; }
    }
}
