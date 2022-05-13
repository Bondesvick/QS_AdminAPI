using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("CORP_ACC_OPENING_SIGNATORY")]
    public class CorpAccOpeningSignatory
    {
        [Key] [JsonIgnore] [Column("ID")] public long Id { get; set; }
        [Column("BVN")] [StringLength(11)] public string Bvn { get; set; }

        [Column("CUSTOMER_REQ_ID")]
        [JsonIgnore]
        public long CustomerReqId { get; set; }

        [Column("TITLE")] public string Title { get; set; }
        [Column("SIGNTORY_NAME")] public string SigntoryName { get; set; }
        [Column("DATE_OF_BIRTH")] public string DateOfBirth { get; set; }
        [Column("GENDER")] public string Gender { get; set; }
        [Column("NATIONALITY")] public string Nationality { get; set; }
        [Column("COUNTRY_OF_RESIDENCE")] public string CountryOfResidence { get; set; }
        [Column("PHONENUMBER")] public string Phonenumber { get; set; }
        [Column("EMAIL")] public string Email { get; set; }
        [Column("MEAN_OF_ID")] public string MeanOfId { get; set; }
        [Column("ID_NUMBER")] public string IdNumber { get; set; }
        [Column("OCCUPATION")] public string Occupation { get; set; }
        [Column("POSITION_OF_OFFICER")] public string PositionOfOfficer { get; set; }

        [Column("DOC_TYPE")]
        [StringLength(100)]
        public string DocType { get; set; }

        [Column("CITY")] public string City { get; set; }
        [Column("STATE")] public string State { get; set; }
        [Column("LGA")] public string Lga { get; set; }
        [Column("UPLOADE_ID")] public string UploadeId { get; set; }
        [Column("UPLOAD_SIGNATURE")] public string UploadSignature { get; set; }
        [Column("UPLOAD_PASSPORT")] public string UploadPassport { get; set; }

        [ForeignKey(nameof(CustomerReqId))]
        [JsonIgnore]
        [InverseProperty(nameof(CustomerRequest.CorpAccOpeningSignatory))]
        public virtual CustomerRequest CustomerReq { get; set; }
    }
}