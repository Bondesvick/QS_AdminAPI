using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("KYC_DOCUMENT_DOCS")]
    public class KycDocumentDocs
    {
        [Key] [Column("ID")] public long Id { get; set; }
        [Column("DOCUMENT_NAME")] public string DocumentName { get; set; }
        [Column("DOCUMENT_TYPE")] public string DocumentType { get; set; }
        [Column("DOCUMENT_NUMBER")] public string DocumentNumber { get; set; }
        [Column("DOCUMENT_FULL_NAME")] public string DocumentFullName { get; set; }
        [Column("DOCUMENT_FILE")] public string DocumentFile { get; set; }
        public long? KycDocumentDetailId { get; set; }
        [Column("DOCUMENT_CONTENT_TYPE")] public string DocumentContentType { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(KycDocumentDetailId))]
        [InverseProperty(nameof(KycDocumentDetails.KycDocumentDocs))]
        public virtual KycDocumentDetails KycDocumentDetail { get; set; }
    }
}