using System.ComponentModel.DataAnnotations;

namespace QuickServiceAdmin.Core.Model
{
    public class EmailMessageRequestDto

    {
        public string FromAddress { get; set; } = "CustomerCareNigeria@stanbicibtc.com";

        [Required] public string ToAddress { get; set; }
        public string CcAddresss { get; set; }

        public string BCc { get; set; }

        public string Subject { get; set; } = "Stanbic IBTC Quick Services";
        public string ContentType { get; set; } = "text/html";

        [Required] public string MailBody { get; set; }
    }
}