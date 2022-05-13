using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("DATA_UPDATE_DOCS")]
    public class DataUpdateDocs
    {
        [Key] [Column("ID")] public int Id { get; set; }
        [Column("DATA_UPDATE_REQ_ID")] public int DataUpdateReqId { get; set; }


        [Column("FILE_NAME")]
        [StringLength(200)]
        public string FileName { get; set; }

        [Column("TITLE")] [StringLength(250)] public string Title { get; set; }
        public string ContentOrPath { get; set; }

        [Column("DOCUMENT_CONTENT_TYPE")]
        [StringLength(250)]
        public string DocumentContentType { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(DataUpdateReqId))]
        [InverseProperty(nameof(DataUpdateDetails.DataUpdateDocs))]
        public virtual DataUpdateDetails DataUpdateReq { get; set; }
    }
}