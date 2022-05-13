using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("CORP_ACC_OPENING__DIRECTOR_DETAILS")]
    public class CorpAccOpeningDirectorDetails
    {
        [Key] [Column("ID")] [JsonIgnore] public long Id { get; set; }

        [Column("CUSTOMER_REQ_ID")]
        [JsonIgnore]
        public long CustomerReqId { get; set; }


        [Column("BVN")]
        [StringLength(11)]
        public string Bvn { get; set; }

        [Column("TITLE")] [StringLength(100)] public string Title { get; set; }

        [Column("NAME_OF_DIRECTOR")]
        [StringLength(100)]
        public string NameOfDirector { get; set; }

        [Column("DATE_OF_BIRTH")] public DateTime DateOfBirth { get; set; }

        [Column("NATIONALITY")]
        [StringLength(100)]
        public string Nationality { get; set; }

        [Column("GENDER")] [StringLength(500)] public string Gender { get; set; }

        [Column("COUNTRY_OF_REDIDENCE")]
        [StringLength(500)]
        public string CountryOfRedidence { get; set; }

        [Column("PHONE_NUMBER")]
        [StringLength(100)]
        public string PhoneNumber { get; set; }

        [Column("MEANS_OF_ID")]
        [StringLength(100)]
        public string MeansOfId { get; set; }

        [Column("ID_NUMBER")]
        [StringLength(100)]
        public string IdNumber { get; set; }

        [Column("OCCUPATION")]
        [StringLength(100)]
        public string Occupation { get; set; }

        [Column("JOB_TITLE")]
        [StringLength(100)]
        public string JobTitle { get; set; }

        [Column("POSITION_OF_OFFICER")]
        [StringLength(100)]
        public string PositionOfOfficer { get; set; }

        [Column("ADDRESS")]
        [StringLength(500)]
        public string Address { get; set; }

        [Column("CITY_TOWN")]
        [StringLength(500)]
        public string CityTown { get; set; }

        [Column("STATE")] [StringLength(100)] public string State { get; set; }
        [Column("LGA")] [StringLength(100)] public string Lga { get; set; }

        [ForeignKey(nameof(CustomerReqId))]
        [JsonIgnore]
        [InverseProperty(nameof(CustomerRequest.CorpAccOpeningDirectorDetails))]
        public virtual CustomerRequest CustomerReq { get; set; }
    }
}