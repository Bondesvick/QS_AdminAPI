using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("ADD_ACC_OPENING_DOCS")]
    public class AddAccOpeningDocs
    {
        [Key] [Column("ID")] public int Id { get; set; }
        [Column("ACC_OPENING_REQ_ID")] public int AccOpeningReqId { get; set; }


        [Column("FILE_NAME")]
        [StringLength(200)]
        public string FileName { get; set; }

        [Column("TITLE")] [StringLength(250)] public string Title { get; set; }
        public string ContentOrPath { get; set; }

        [Column("DOCUMENT_CONTENT_TYPE")]
        [StringLength(250)]
        public string DocumentContentType { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(AccOpeningReqId))]
        [InverseProperty(nameof(AddAccOpeningDetails.AddAccOpeningDocs))]
        public virtual AddAccOpeningDetails AccOpeningReq { get; set; }
    }
}