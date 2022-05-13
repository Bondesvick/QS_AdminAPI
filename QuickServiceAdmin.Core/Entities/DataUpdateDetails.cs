using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("DATA_UPDATE_DETAILS")]
    public class DataUpdateDetails
    {
        public DataUpdateDetails()
        {
            DataUpdateDocs = new HashSet<DataUpdateDocs>();
        }

        [Key] [Column("ID")] [JsonIgnore] public int Id { get; set; }

        [Column("CUSTOMER_REQ_ID")]
        [JsonIgnore]
        public long CustomerReqId { get; set; }

        [Column("REQUESTED_ACC_TYPE")]
        [StringLength(100)]
        public string RequestedAccType { get; set; }

        [Column("EXISTING_ACC_TYPE")]
        [StringLength(100)]
        public string ExistingAccType { get; set; }

        [Column("EXISTING_ACC_SEGMENT")]
        [StringLength(100)]
        public string ExistingAccSegment { get; set; }

        [Column("CURRENCY")]
        [StringLength(50)]
        public string Currency { get; set; }

        [Column("BVN")] [StringLength(20)] public string Bvn { get; set; }

        [Column("PHONE_NUMBER")]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [Column("EXTRA_ACC_CLASS")]
        [StringLength(50)]
        public string ExtraAccClass { get; set; }

        [Column("ID_TYPE")]
        [StringLength(100)]
        public string IdType { get; set; }

        [Column("ID_NUMBER")]
        [StringLength(100)]
        public string IdNumber { get; set; }

        [Column("CASE_ID")] [StringLength(50)] public string CaseId { get; set; }

        [Column("CURRENT_STEP")]
        [StringLength(100)]
        public string CurrentStep { get; set; }

        [Column("SUBMITTED")] public bool? Submitted { get; set; }

        [Column("DATA_TO_UPDATE")]
        [StringLength(100)]
        public string DataToUpdate { get; set; }

        [Column("HOUSE_NUMBER")]
        [StringLength(100)]
        public string HouseNumber { get; set; }

        [Column("STREET_NAME")]
        [StringLength(100)]
        public string StreetName { get; set; }

        [Column("CITY_TOWN")]
        [StringLength(100)]
        public string CityTown { get; set; }

        [Column("STATE")] [StringLength(100)] public string State { get; set; }
        [Column("LGA")] [StringLength(100)] public string Lga { get; set; }

        [Column("BUS_STOP")]
        [StringLength(100)]
        public string BusStop { get; set; }

        [Column("ALIAS")] [StringLength(100)] public string Alias { get; set; }

        [Column("HOUSE_DESCRIPTION")]
        [StringLength(100)]
        public string HouseDescription { get; set; }

        [Column("NEW_PHONE_NUMBER")]
        [StringLength(100)]
        public string NewPhoneNumber { get; set; }

        [Column("NEW_EMAIL")]
        [StringLength(100)]
        public string NewEmail { get; set; }

        [Column("TIN")]
        public string TinNumber {get; set; }

        [Column("COUNTRY_CODE")]
        public string CountryCode {get; set; }

        [Column("I_ACCEPT_TERMS_AND_CONDITIONS")]
        public bool? AcceptTermsAndConditions { get; set; }

        [Column("DATE_OF_ACCEPTING_T_AND_C", TypeName = "datetime")]
        public DateTime? DateOfAcceptingTAndC { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CustomerReqId))]
        [InverseProperty(nameof(CustomerRequest.DataUpdateDetails))]
        public virtual CustomerRequest CustomerReq { get; set; }

        [InverseProperty("DataUpdateReq")] public virtual ICollection<DataUpdateDocs> DataUpdateDocs { get; set; }
    }
}