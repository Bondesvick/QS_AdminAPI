using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("SME_ONBOARDING_DOCS")]
    public class SmeOnboardingDocs
    {
        [Key] [Column("ID")] public int Id { get; set; }
        [Column("REQ_ID")] public int ReqId { get; set; }


        [Column("DOC_TYPE")]
        [StringLength(100)]
        public string DocType { get; set; }


        [Column("DOC_EXTENSION")]
        [StringLength(15)]
        public string DocExtension { get; set; }

        [Column("DOC_CONTENT")] public string DocContent { get; set; }

        [ForeignKey(nameof(ReqId))]
        [InverseProperty(nameof(SmeOnboardingDetails.SmeOnboardingDocs))]
        public virtual SmeOnboardingDetails Req { get; set; }
    }
}