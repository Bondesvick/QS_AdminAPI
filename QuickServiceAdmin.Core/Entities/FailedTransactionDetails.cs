using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("FAILED_TRANSACTION_DETAILS")]
    public class FailedTransactionDetails
    {
        [Key] [Column("ID")] [JsonIgnore] public long Id { get; set; }


        [Column("TRANSACTION_TYPE")]
        [StringLength(100)]
        public string TransactionType { get; set; }

        [Column("TRANSACTION_DATE")] public DateTime TransactionDate { get; set; }


        [Column("ACCOUNT_NUMBER")]
        [StringLength(10)]
        public string AccountNumber { get; set; }

        [Column("ADDITIONAL_DETAILS")]
        [StringLength(150)]
        public string AdditionalDetails { get; set; }

        [Column("CUSTOMER_REQUEST_ID")]
        [JsonIgnore]
        public long CustomerRequestId { get; set; }

        [Column("BENEFICIARY_BANK")]
        [StringLength(200)]
        public string BeneficiaryBank { get; set; }

        [Column("AMOUNT", TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }

        [ForeignKey(nameof(CustomerRequestId))]
        [JsonIgnore]
        [InverseProperty("FailedTransactionDetails")]
        public virtual CustomerRequest CustomerRequest { get; set; }
    }
}