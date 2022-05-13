using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickServiceAdmin.Core.Interface;
using QuickServiceAdmin.Core.Model;
using Microsoft.Extensions.Configuration;

namespace QuickServiceAdmin.Core.Services
{
    public class FacialIdentityRequestService : IFacialIdentityRequestService
    {
        private readonly IJsonRequestHelper _jsonRequestHelper;
        private readonly ILogger<FacialIdentityRequestService> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _urlPath;

        public FacialIdentityRequestService(IJsonRequestHelper jsonRequestHelper, ILogger<FacialIdentityRequestService> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _jsonRequestHelper = jsonRequestHelper;
            _logger = logger;
            _httpClient = httpClient;
            _urlPath = configuration["AppSettings:FacialIdentityRequestServiceConfig:Path"];
        }

        public async Task<FacialIdentityResponseData> RequestFacialIdentity(FacialIdentityRequestParams facialIdentityRequestParams)
        {
            var jsonRequestString = JsonConvert.SerializeObject(facialIdentityRequestParams);

            _logger.LogInformation("Facial Identity Request: " + jsonRequestString);

            var request = new StringContent(jsonRequestString, Encoding.UTF8, "application/json");

            var response =
                await _jsonRequestHelper.MakeJsonRequest("POST", _urlPath, _httpClient,
                    request);

            _logger.LogInformation("Facial Identity Response: " + response);

            var facialIdentityResponse = JsonConvert.DeserializeObject<JObject>(response);

            var statusCode = (int) facialIdentityResponse.SelectToken("status_code");

            if (statusCode != 200)
            {
                var message = (string)facialIdentityResponse.SelectToken("message");
                throw new CustomErrorException(message, ResponseCodeConstants.BadRequest);
            }

            return facialIdentityResponse.ToObject<FacialIdentityRequestDetailsResponse>().Data;
        }

    }
}