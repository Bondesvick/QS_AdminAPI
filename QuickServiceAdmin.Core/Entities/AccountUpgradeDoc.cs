using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("ACCOUNT_UPGRADE_DOC")]
    public class AccountUpgradeDoc
    {
        [Key] [JsonIgnore] [Column("ID")] public long Id { get; set; }


        [Column("DOC_TYPE")]
        [StringLength(10)]
        public string DocType { get; set; }

        [Column("DOC_EXTENSION")]
        [StringLength(20)]
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
        [InverseProperty(nameof(CustomerRequest.AccountUpgradeDoc))]
        public virtual CustomerRequest CustomerReq { get; set; }
    }
}