using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Model
{
    public class FaceDetails
    {
        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("threshold")]
        public int Threshold { get; set; }

        [JsonProperty("request_image")]
        public string RequestImage { get; set; }
    }

    public class Response
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("dob")]
        public string Dob { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }

        [JsonProperty("face_details")]
        public FaceDetails FaceDetails { get; set; }
    }

    public class FacialIdentityResponseData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        [JsonProperty("response")]
        public Response Response { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("task_created_by")]
        public string TaskCreatedBy { get; set; }
    }

    public class FacialIdentityRequestDetailsResponse
    {
        [JsonProperty("data")]
        public FacialIdentityResponseData Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("status_code")]
        public int StatusCode { get; set; }
    }
}