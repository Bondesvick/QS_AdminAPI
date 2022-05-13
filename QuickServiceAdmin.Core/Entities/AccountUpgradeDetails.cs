using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("ACCOUNT_UPGRADE_DETAILS")]
    public class AccountUpgradeDetails
    {
        [JsonIgnore] [Key] [Column("ID")] public long Id { get; set; }

        [Column("PHONE_NUMBER")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Column("OLD_SCHEME")]
        [StringLength(100)]
        public string OldScheme { get; set; }

        [Column("NEW_SCHEME")]
        [StringLength(100)]
        public string NewScheme { get; set; }

        [Column("NEW_ADDRESS")]
        [StringLength(1000)]
        public string NewAddress { get; set; }

        [Column("ID_TYPE")]
        [StringLength(500)]
        public string IdType { get; set; }

        [Column("ID_NUMBER")]
        [StringLength(100)]
        public string IdNumber { get; set; }

        [Column("HAVE_DEBIT_CREDIT")]
        [StringLength(10)]
        public string HaveDebitCredit { get; set; }

        [Column("CUSTOMER_REQ_ID")]
        [JsonIgnore]
        public long CustomerReqId { get; set; }

        [Column("CREATED_AT")] public DateTime CreatedAt { get; set; }
        [Column("PUBLICHED_BY")] public string PublichedBy { get; set; }
        [Column("UNDATING ADD")] public DateTime UndatingAdd { get; set; }
        [Column("STEP_INDEX")] public int StepIndex { get; set; }
        [Column("SUBMITTED")] public bool? Submitted { get; set; }
        public string Bvn { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CustomerReqId))]
        [InverseProperty(nameof(CustomerRequest.AccountUpgradeDetails))]
        public virtual CustomerRequest CustomerReq { get; set; }
    }
}