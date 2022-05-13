using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("CUSTOMER_REQUEST_DOCUMENTS")]
    public class CustomerRequestDocuments
    {
        [Key] [Column("ID")] public long Id { get; set; }

        [Required]
        [Column("DOCUMENT_FILENAME")]
        public string DocumentFilename { get; set; }

        [Required] [Column("DOCUMENT_TYPE")] public string DocumentType { get; set; }
        [Required] [Column("DOCUMENT_FILE")] public string DocumentFile { get; set; }
        [JsonIgnore] public long CustomerRequestId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CustomerRequestId))]
        [InverseProperty("CustomerRequestDocuments")]
        public virtual CustomerRequest CustomerRequest { get; set; }
    }
}