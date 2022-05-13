using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("LOAN_REPAYMENT_DOC")]
    public partial class LoanRepaymentDocument
    {
        [Key] [Column("ID")] [JsonIgnore] public long Id { get; set; }
        public string DocTitle { get; set; } //title of document
        [Column("DOC_EXTENSION")] public string DocExtension { get; set; }
        [Column("DOC_NAME")] public string DocName { get; set; } //name of document is signature
        [Column("DOC_CONTENT")] public string DocContent { get; set; }

        [Column("CUSTOMER_REQ_ID")]
        public long CustomerRequestId { get; set; }

        [ForeignKey(nameof(CustomerRequestId))]
        [InverseProperty("LoanRepaymentDocuments")]
        [JsonIgnore]
        public virtual CustomerRequest CustomerRequest { get; set; }
    }
}