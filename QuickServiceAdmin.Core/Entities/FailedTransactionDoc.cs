using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("FAILED_TRANSACTION_DOC")]
    public class FailedTransactionDoc
    {
        [Key] [JsonIgnore] [Column("ID")] public long Id { get; set; }


        [Column("DOC_EXTENSION")]
        [StringLength(10)]
        public string DocExtension { get; set; }


        [Column("DOC_NAME")]
        [StringLength(500)]
        public string DocName { get; set; }

        [Column("DOC_CONTENT")] public string DocContent { get; set; }

        [Column("CUSTOMER_REQ_ID")]
        [JsonIgnore]
        public long CustomerReqId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CustomerReqId))]
        [InverseProperty(nameof(CustomerRequest.FailedTransactionDoc))]
        public virtual CustomerRequest CustomerReq { get; set; }
    }
}