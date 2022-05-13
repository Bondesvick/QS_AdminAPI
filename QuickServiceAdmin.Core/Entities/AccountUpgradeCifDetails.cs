using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("ACCOUNT_UPGRADE_CIF_DETAILS")]
    public class AccountUpgradeCifDetails
    {
        [JsonIgnore] [Key] [Column("ID")] public long Id { get; set; }

        [JsonIgnore]
        [Column("CUSTOMER_REQ_ID")]
        public long CustomerReqId { get; set; }

        [Column("PLACE OF BIRTH")] public string PlaceOfBirth { get; set; }
        [Column("MOTHER_MAIDEN_NAME")] public string MotherMaidenName { get; set; }
        [Column("CITY")] [StringLength(1000)] public string City { get; set; }
        [Column("STATE")] public string State { get; set; }
        [Column("NATIONALITY")] public string Nationality { get; set; }
        [Column("EMAIL")] public string Email { get; set; }
        [Column("DATE_OF_BIRTH")] public string DateOfBirth { get; set; }
        [Column("GENDER")] public string Gender { get; set; }
        [Column("HOUSE_NUMBER")] public string HouseNumber { get; set; }
        [Column("STREET_NAME")] public string StreetName { get; set; }
        [Column("HOME_ADDRESS")] public string HomeAddress { get; set; }
        public string ContactState { get; set; }
        [Column("CONTACT_DESC")] public string ContactDesc { get; set; }
        [Column("CONTACT_ALLIASE")] public string ContactAlliase { get; set; }
        [Column("NOB_FULLNAME")] public string NobFullname { get; set; }
        [Column("NOK_ADDRESS")] public string NokAddress { get; set; }

        [Column("NOB_PHONENUMBER")]
        [StringLength(15)]
        public string NobPhonenumber { get; set; }

        [Column("NOB_EMAIL")] public string NobEmail { get; set; }
        [Column("NOB_GENDER")] public string NobGender { get; set; }
        [Column("EMPLOYMENT_STATUS")] public string EmploymentStatus { get; set; }
        [Column("NATURE_OF_BUSINESS")] public string NatureOfBusiness { get; set; }
        [Column("INCOME_RANGE")] public string IncomeRange { get; set; }
        public string ProminentPubFam { get; set; }
        [Column("EMPLOYER_NAME")] public string EmployerName { get; set; }
        [Column("PROMINENT_INT_FAM")] public string ProminentIntFam { get; set; }
        [Column("OCCUPATION")] public string Occupation { get; set; }
        [Column("CREATED_AT")] public DateTime CreatedAt { get; set; }
        [Column("UPDATED AT")] public DateTime UpdatedAt { get; set; }
        [Column("PUBLISHED_BY")] public string PublishedBy { get; set; }
        public string Description { get; set; }
        [Column("NOKDateOfBirth")] public string NokdateOfBirth { get; set; }
        [Column("NOKRelation")] public string Nokrelation { get; set; }
        public string NearestBustStop { get; set; }
        public string EducationLevel { get; set; }
        public string Institute { get; set; }
        public string Industry { get; set; }
        [Column("CONTACT_LGA")] public string ContactLga { get; set; }
        [Column("TOWNCITY")] public string Towncity { get; set; }

        [Column("PROM_INT_PERSON_NAME")]
        [StringLength(50)]
        public string PromIntPersonName { get; set; }

        [Column("PROM_INT_PERSON_POSITION")]
        [StringLength(50)]
        public string PromIntPersonPosition { get; set; }

        [Column("PROM_INT_PERSON_RELATIONSHIP")]
        [StringLength(50)]
        public string PromIntPersonRelationship { get; set; }

        [Column("PROM_PUB_PERSON_NAME")]
        [StringLength(50)]
        public string PromPubPersonName { get; set; }

        [Column("PROM_PUB_PERSON_POSITION")]
        [StringLength(50)]
        public string PromPubPersonPosition { get; set; }

        [Column("PROM_PUB_PERSON_RELATIONSHIP")]
        [StringLength(50)]
        public string PromPubPersonRelationship { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CustomerReqId))]
        [InverseProperty(nameof(CustomerRequest.AccountUpgradeCifDetails))]
        public virtual CustomerRequest CustomerReq { get; set; }
    }
}