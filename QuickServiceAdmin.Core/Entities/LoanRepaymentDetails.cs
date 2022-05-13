using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("LOAN_REPAYMENT_TRANSACTION_DETAILS")]
    public class LoanRepaymentDetails
    {
        [Key] [Column("ID")] [JsonIgnore] public long Id { get; set; }
        [Column("ACCOUNT_NAME")] public string AccountName { get; set; }
        [Column("ACCOUNT_NUMBER")] public string AccountNumber { get; set; }
        [Column("SIGNATURE_CONTENT")] public string SignatureContent { get; set; }
        public string SignatureExt { get; set; }
        [Column("REPAYMENT_PLAN")] public string RepaymentPlan { get; set; }
        [Column("ACCOUNT_SEGMENT")] public string AccountSegment { get; set; }
        [Column("REPAYMENT_AMOUNT")] public string RepaymentAmount { get; set; }
        [Column("LOAN_ACCOUNT_NO")] public string LoanAccountNo { get; set; }
        [Column("LOAN_CURRENT_BALANCE")] public string LoanCurrentBalance { get; set; }
        [Column("REPAYMENT_ACCOUNT_NO")] public string RepaymentAcctNo { get; set; }
        [Column("AMOUNT")] public string Amount { get; set; }
        [Column("HAVE_DEBIT_CARD")] public string HaveDebitCard { get; set; }

        //public List<LoanRepaymentModel> LoanRepaymentRequestDetails { get; set; }
        [ForeignKey(nameof(CustomerRequestId))]
        [InverseProperty("LoanRepaymentDetails")]
        [JsonIgnore]
        public virtual CustomerRequest CustomerRequest { get; set; }

        [Column("CUSTOMER_REQUEST_ID")]
        public long CustomerRequestId { get; set; }
    }
}