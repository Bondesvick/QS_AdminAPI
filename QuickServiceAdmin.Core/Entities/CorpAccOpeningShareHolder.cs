using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("CORP_ACC_OPENING_SHARE_HOLDER")]
    public class CorpAccOpeningShareHolder
    {
        [Key] [Column("ID")] [JsonIgnore] public long Id { get; set; }

        [Column("CUSTOMER_REQ_ID")]
        [JsonIgnore]
        public long CustomerReqId { get; set; }

        [Column("SIG_WITH_ABOVE_FIVE_PER")] public string SigWithAboveFivePer { get; set; }
        [Column("BVN")] [StringLength(11)] public string Bvn { get; set; }
        [Column("TITLE")] public string Title { get; set; }
        [Column("SHARE_HOLDER_NAME")] public string ShareHolderName { get; set; }
        [Column("MEANS_OF_ID")] public string MeansOfId { get; set; }

        [Column("IDNUMBER")]
        [StringLength(50)]
        public string Idnumber { get; set; }

        [Column("ADDRESS")]
        [StringLength(100)]
        public string Address { get; set; }

        [ForeignKey(nameof(CustomerReqId))]
        [JsonIgnore]
        [InverseProperty(nameof(CustomerRequest.CorpAccOpeningShareHolder))]
        public virtual CustomerRequest CustomerReq { get; set; }
    }
}