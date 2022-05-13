using System.ComponentModel.DataAnnotations;

namespace QuickServiceAdmin.Core.Model
{
    public class GetCustomerRequestParams
    {
        [Required] public string Module { get; set; }
        [Required] public string TicketId { get; set; }
    }
}