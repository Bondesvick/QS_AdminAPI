using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("LOAN_REQUEST_DOC")]
    public partial class LoanRequestDoc
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("DOC_EXTENSION")]
        [StringLength(10)]
        public string DocExtension { get; set; }
        [Column("DOC_NAME")]
        public string DocName { get; set; }
        [Column("DOC_CONTENT")]
        public string DocContent { get; set; }
        [Column("CUSTOMER_REQ_ID")]
        public long CustomerReqId { get; set; }

        [ForeignKey(nameof(CustomerReqId))]
        [InverseProperty(nameof(CustomerRequest.LoanRequestDoc))]
        [JsonIgnore]
        public virtual CustomerRequest CustomerReq { get; set; }
    }
}
