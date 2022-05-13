using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("REQUEST_AND_STATUS_MGT_DETAILS")]
    public partial class RequestAndStatusMgtDetails
    {
        public RequestAndStatusMgtDetails()
        {
            RequestAndStatusMgtDocs = new HashSet<RequestAndStatusMgtDocs>();
        }

        [JsonIgnore]
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("CUSTOMER_REQ_ID")]

        [JsonIgnore]
        public long CustomerReqId { get; set; }
        [Column("ACCOUNT_NUMBER")]
        [StringLength(50)]
        public string AccountNumber { get; set; }
        [Column("REMARKS")]
        [StringLength(500)]
        public string Remarks { get; set; }
        [Column("REQUEST_TYPE")]
        [StringLength(50)]
        public string RequestType { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CustomerReqId))]
        [InverseProperty("RequestAndStatusMgtDetails")]
        public virtual CustomerRequest CustomerRequest { get; set; }

        [InverseProperty("RequestAndStatusMgt")]
        public virtual ICollection<RequestAndStatusMgtDocs> RequestAndStatusMgtDocs { get; set; }
    }
}
