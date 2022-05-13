using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("CORP_ACC_OPENING_ENTER_ONLINE")]
    public class CorpAccOpeningEnterOnline
    {
        [Key] [Column("ID")] [JsonIgnore] public long Id { get; set; }

        [Column("CUSTOMER_REQ_ID")]
        [JsonIgnore]
        public long CustomerReqId { get; set; }

        [Column("ENTERPRICE _PROFILE")] public string EnterpriceProfile { get; set; }
        [Column("COMP_TRANS_LIMIT")] public string CompTransLimit { get; set; }
        [Column("MANDATE")] public string Mandate { get; set; }
        [Column("FULLNAME")] public string Fullname { get; set; }
        [Column("MOBILE_NUMBER")] public string MobileNumber { get; set; }
        [Column("EMAIL_ADDRESS")] public string EmailAddress { get; set; }
        [Column("ONE_TIME_PASS_MEAN")] public string OneTimePassMean { get; set; }
        [Column("MAX_USER_TRANS_LIMIT")] public string MaxUserTransLimit { get; set; }
        [Column("ACCESSIBLE_MENU_CODE")] public string AccessibleMenuCode { get; set; }
        [Column("ROLE")] public string Role { get; set; }

        [ForeignKey(nameof(CustomerReqId))]
        [JsonIgnore]
        [InverseProperty(nameof(CustomerRequest.CorpAccOpeningEnterOnline))]
        public virtual CustomerRequest CustomerReq { get; set; }
    }
}