using System.ComponentModel.DataAnnotations;

namespace QuickServiceAdmin.Core.Model
{
    public class AssignPayload
    {
        [Required] public string AssignedBy { get; set; }
        [Required] public string AssignedTo { get; set; }
        [Required] public string Status { get; set; }
        public string Remarks { get; set; }
    }
}