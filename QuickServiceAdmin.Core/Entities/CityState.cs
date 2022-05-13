using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("CITY_STATE")]
    public partial class CityState
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("city")]
        [StringLength(200)]
        public string City { get; set; }

        [Column("city_ID")]
        [StringLength(50)]
        public string CityId { get; set; }

        [Column("region")]
        [StringLength(200)]
        public string Region { get; set; }

        [Column("region_id")]
        [StringLength(50)]
        public string RegionId { get; set; }

        [Column("region_code")]
        [StringLength(50)]
        public string RegionCode { get; set; }

        [Column("country_code")]
        [StringLength(50)]
        public string CountryCode { get; set; }
        [Column("country_code_debit")]
        [StringLength(50)]
        public string CountryCodeDebit { get; set; }
        [Column("country")]
        [StringLength(50)]
        public string Country { get; set; }
    }
}
