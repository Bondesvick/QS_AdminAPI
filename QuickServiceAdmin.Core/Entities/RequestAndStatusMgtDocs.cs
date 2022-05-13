using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("REQUEST_AND_STATUS_MGT_DOCS")]
    public partial class RequestAndStatusMgtDocs
    {
        [JsonIgnore]
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("REQUEST_AND_STATUS_MGT_ID")]
        [JsonIgnore]
        public long RequestAndStatusMgtId { get; set; }

        public string ContentOrPath { get; set; }

        [Column("FILE_NAME")]
        [StringLength(100)]
        public string FileName { get; set; }

        [Column("TITLE")]
        [StringLength(100)]
        public string Title { get; set; }

        [Column("DOCUMENT_CONTENT_TYPE")]
        [StringLength(100)]
        public string DocumentContentType { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(RequestAndStatusMgtId))]
        [InverseProperty(nameof(RequestAndStatusMgtDetails.RequestAndStatusMgtDocs))]
        public virtual RequestAndStatusMgtDetails RequestAndStatusMgt { get; set; }
    }
}
