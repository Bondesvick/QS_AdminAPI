using System.ComponentModel.DataAnnotations;

namespace QuickServiceAdmin.Core.Model
{
    public class TreatPayload
    {
        [Required] public string TreatedBy { get; set; }
        [Required] public string Status { get; set; }
        public string Remarks { get; set; }
    }
}