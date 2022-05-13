using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("LOAN_REQUEST_DETAILS")]
    public partial class LoanRequestDetails
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("LOAN_AMOUNT", TypeName = "decimal(18, 2)")]
        public decimal LoanAmount { get; set; }
        [Column("LOAN_TENURE")]
        public int LoanTenure { get; set; }

        [Column("ACCOUNT_NUMBER")]
        [StringLength(10)]
        public string AccountNumber { get; set; }
        [Column("REPAYMENT_ACCOUNT_NUMBER")]
        [StringLength(10)]
        public string RepaymentAccountNumber { get; set; }
        [Column("REPAYMENT_DAY")]
        public short RepaymentDay { get; set; }
        [Column("PLPP")]
        public string Plpp { get; set; }

        [Column("ACCOUNT_SEGMENT")]
        [StringLength(100)]
        public string AccountSegment { get; set; }
        [Column("CUSTOMER_REQUEST_ID")]
        public long CustomerRequestId { get; set; }
        public string ProductName { get; set; }

        [Column("ACCOUNT_NAME")]
        [StringLength(1000)]
        public string AccountName { get; set; }
        [Column("DISBURSED_INTO_ACCOUNT_NUMBER")]
        public string DisbursedIntoAccountNumber { get; set; }
        [Column("CURRENCY")]
        public string Currency { get; set; }

        [ForeignKey(nameof(CustomerRequestId))]
        [InverseProperty("LoanRequestDetails")]
        [JsonIgnore]
        public virtual CustomerRequest CustomerRequest { get; set; }
    }
}
