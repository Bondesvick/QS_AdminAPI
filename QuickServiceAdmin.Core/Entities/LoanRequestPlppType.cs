using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("LOAN_REQUEST_PLPP_TYPE")]
    public partial class LoanRequestPlppType
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }
    }
}
