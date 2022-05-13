using Newtonsoft.Json.Linq;

namespace QuickServiceAdmin.Core.Model
{
    public class CardRequestDetailsResponse
    {
        public JToken Currencies { get; set; }
        public JToken Cities { get; set; }
        public JToken Genders { get; set; }
        public JToken Titles { get; set; }
        public JToken MaritalStatuses { get; set; }
        public JToken Branches { get; set; }
        public JToken EligibleCards { get; set; }
    }
}