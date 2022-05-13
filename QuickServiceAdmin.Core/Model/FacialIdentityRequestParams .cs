using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Model
{
    public class FacialIdentityRequestParams
    {
        // { 
        //  "report_type": "identity", 
        //  "type": "ibvn", 
        //  "reference": "xxxxxxxxxxx", [required][String]
        //  "last_name": "John", [ optional ] 
        //  "first_name": "Doe", [ optional ] 
        //  "dob": "2000-01-01", [ optional ] 
        //  "subject_consent": true [required] [Boolean] 
        // }
        [JsonProperty("report_type")]
        public string ReportType { get; set; } = "identity";
        [JsonProperty("type")]
        public string IdentityType { get; set; }
        [JsonProperty("reference")]
        public string IdentityNumber { get; set; }
        [JsonProperty("image")]
        public string RequestImage { get; set; }
        [JsonProperty("subject_consent")]
        public bool SubjectConsent { get; set; } = true;
    }
}