using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("LOAN_PRODUCT_DOCUMENT")]
    public partial class LoanProductDocument
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("PRODUCT_CATEGORY")]
        public string ProductCategory { get; set; }

        [Column("PRODUCT_NAME")]
        public string ProductName { get; set; }

        [Column("DOCUMENT_NAME")]
        public string DocumentName { get; set; }

        [Column("DOCUMENT_CODE")]
        [StringLength(20)]
        public string DocumentCode { get; set; }
        [Column("IS_REQUIRED")]
        public bool IsRequired { get; set; }

        [Column("NOTE")]
        public string Note { get; set; }
    }
}
