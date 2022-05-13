using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("FACIAL_IDENTITY_REQUEST_DETAILS")]
    public class FacialIdentityRequestDetails
    {
        [JsonIgnore]
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("STATUS")] [Required] public string Status { get; set; } = "IDLE";

        [Column("COMMENT")]
        public string Comment { get; set; }

        [Column("INITIATED_BY")]
        [Required] public string InitiatedBy { get; set; }

        [Column("APPROVED_BY")]
        public string ApprovedBy { get; set; }

        [Column("REPORT_ID")]
        public string ReportId { get; set; }

        [Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [Column("MIDDLE_NAME")]
        public string MiddleName { get; set; }

        [Column("LAST_NAME")]
        public string LastName { get; set; }

        [Column("DATE_OF_BIRTH")]
        public string DateOfBirth { get; set; }

        [Column("REQUEST_PHOTO")]
        public string RequestPhoto { get; set; }

        [Column("CONFIDENCE")]
        public double Confidence { get; set; }

        [Column("IDENTITY_TYPE")]
        public string IdentityType { get; set; }

        [Column("IDENTITY_NUMBER")]
        public string IdentityNumber { get; set; }

        [Column("THRESHOLD")]
        public double Threshold { get; set; }

        [Column("RESPONSE_PHOTO")]
        public string ResponsePhoto { get; set; }

        [Column("CUSTOMER_REQUEST_ID")]
        [Required]
        [JsonIgnore] public long CustomerRequestId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CustomerRequestId))]
        [InverseProperty("FacialIdentityRequestDetails")]
        public virtual CustomerRequest CustomerRequest { get; set; }
    }
}
