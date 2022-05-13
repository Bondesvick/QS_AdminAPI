using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("DEBIT_CARD_DOCS")]
    public partial class DebitCardDocument
    {
        [Key] [Column("ID")] [JsonIgnore] public int Id { get; set; }

        [Column("FILE_NAME")] public string FileName { get; set; }
        [Column("TITLE")] public string Title { get; set; }
        public string ContentOrPath { get; set; }
        [Column("DOCUMENT_CONTENT_TYPE")] public string ContentType { get; set; }

        //[ForeignKey(nameof(AccOpeningReqId))]
        //[InverseProperty(nameof(DebitCardDetails.Documents))]
        //[JsonIgnore]
        //public virtual DebitCardDetails AccountOpeningRequest { get; set; }

        [Column("DEBIT_CARD_REQ_ID")] public int AccOpeningReqId { get; set; }
    }
}