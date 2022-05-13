using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace QuickServiceAdmin.Core.Model
{
    public class AddressRequestDetailsResponse
    {
        public JToken Cities { get; set; }
        public JToken Branches { get; set; }

        public List<string> States { get; set; }
    }
}