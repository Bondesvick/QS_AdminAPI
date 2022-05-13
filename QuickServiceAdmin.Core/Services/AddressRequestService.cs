using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickServiceAdmin.Core.Entities;
using QuickServiceAdmin.Core.Helpers;
using QuickServiceAdmin.Core.Interface;
using QuickServiceAdmin.Core.Model;

namespace QuickServiceAdmin.Core.Services
{
    [ExcludeFromCodeCoverage]
    public class AddressRequestService : IAddressRequestService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<AddressRequestService> _logger;
        private readonly IJsonRequestHelper _jsonRequestHelper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly QuickServiceContext _db;
        private readonly IRedboxService _redboxService;
        private readonly string _redboxBaseUrl;
        private readonly string _authorization;
        private readonly string _moduleId;



        public AddressRequestService(IRedboxService redboxService, QuickServiceContext db, IHttpClientFactory httpClientFactory, ILogger<AddressRequestService> logger, HttpClient httpClient, IJsonRequestHelper jsonRequestHelper, IConfiguration configuration)
        {
            _redboxService = redboxService;
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
            _jsonRequestHelper = jsonRequestHelper;
            _httpClientFactory = httpClientFactory;
            _db = db;
            _redboxBaseUrl = configuration["AppSettings:RedboxBaseEndPoint"];
            _authorization = configuration["AppSettings:RedboxAuthorization"];
            _moduleId = configuration["AppSettings:RedboxModuleId"];
        }

        public async Task<string> RequestAddress(AddressRequestDto addressRequestDto)
        {
            var xmlRequestString = GetAddressRequestString(addressRequestDto);
            _logger.LogInformation(xmlRequestString);

            var request = new StringContent(xmlRequestString, Encoding.UTF8, "text/xml");


            _httpClient.DefaultRequestHeaders.Add("Authorization", _configuration["AppSettings:AddressRequestServiceConfig:RedboxAuthorization"]);
            _httpClient.DefaultRequestHeaders.Add("SOAPAction",
                _configuration["AppSettings:AddressRequestServiceConfig:SoapAction"]);
            _httpClient.DefaultRequestHeaders.Add("Module_Id",
                _configuration["AppSettings:AddressRequestServiceConfig:RedboxModuleId"]);

            var response = await _jsonRequestHelper.MakeJsonRequest("POST", _configuration["AppSettings:AddressRequestServiceConfig:RedboxEndpoint"],
                _httpClient, request);

            response = response.Replace("&lt;", "<").Replace("&gt;", ">")
                    .Replace($@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>", "")
                    .Replace($@"<?xml version=""1.0""?>", "")
                    .Replace("ns2:", "")
                    .Replace(":ns2", "")
                    .Replace($@" xmlns=""http://soap.request.manager.redbox.stanbic.com/""", "")
                    .Replace($@" xmlns=""http://soap.messaging.outbound.redbox.stanbic.com/""", "")
                    .Replace($@" xmlns=""http://soap.finacle.redbox.stanbic.com/""", "")
                    .Replace("soap:", "")
                    .Replace($@" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""", "")
                ;

            _logger.LogInformation(response);

            var responseCode = RequestHelper.GetFirstTagValue(response, "responseCode");

            if (responseCode == "000")
            {
                var reqInitiationId = RequestHelper.GetFirstTagValue(response, "reqInitiationId");
                return reqInitiationId;
            };

            var description = RequestHelper.GetFirstTagValue(response, "responseDescription");
            var detail = RequestHelper.GetFirstTagValue(response, "detail");

            var message = detail;

            if (string.IsNullOrEmpty(detail))
            {
                message = description;
            }

            throw new CustomErrorException(message, ResponseCodeConstants.BadRequest);

        }
        public async Task<AddressRequestDetailsResponse> GetAddressRequestDetails()
        {
            var request = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient("CardRequestServiceClient");

            var cityTask = _jsonRequestHelper.MakeJsonRequest("GET", "api/carddictionary/getcity", httpClient, request);
            var branchesTask =
                   _jsonRequestHelper.MakeJsonRequest("GET", "api/carddictionary/getbranches", httpClient, request);

            var statesTask = _db.CityState.Select(x => x.Region).Distinct().ToListAsync();

            var results = await Task.WhenAll(cityTask,
                branchesTask);

            var resultClasses = results.Select(JsonConvert.DeserializeObject<JObject>);

            var resultClassesList = resultClasses.ToList();
            if (resultClassesList.Any(result => (string)result.SelectToken("responseCode") != "00"))
            {
                _logger.LogError("Could not get response " + string.Join(" -------------------- ", results));

                var resultClass = resultClassesList.FirstOrDefault(result => (string)result.SelectToken("responseCode") == "99");

                if (resultClass != null)
                {
                    throw new CustomErrorException((string)resultClass.SelectToken("responseDescription"), ResponseCodeConstants.Failure);
                }

                throw new Exception("Something went wrong while getting response for address request details");
            }

            var responseClasses = resultClassesList
                .Select(result => result.SelectToken("data")).ToList();

            var response = new AddressRequestDetailsResponse
            {
                Cities = responseClasses[0],
                Branches = responseClasses[1],
                States = await statesTask
            };

            return response;
        }


        private static string GetAddressRequestString(AddressRequestDto addressRequestDto)
        {
            var reqTranId = Guid.NewGuid();
            return $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:soap=""http://soap.request.manager.redbox.stanbic.com/"">
                        <soapenv:Header/>
                        <soapenv:Body>
                            <soap:request>
                                <!--Optional:-->
                                <reqTranId>{reqTranId}</reqTranId>
                                <!--Optional:-->
                                <channel>BPM</channel>
                                <type>INIT_ADDS_VER</type>
                                <customerId></customerId>
                                <customerIdType></customerIdType>
                                <body>
                                    <![CDATA[
                    <otherRequestDetails><moduleTranReferenceId>{reqTranId}</moduleTranReferenceId><cIFID>{addressRequestDto.CifId}</cIFID><requestType>3</requestType><addressLine1>{addressRequestDto.AddressLine1}</addressLine1><city>{addressRequestDto.City}</city><state>{addressRequestDto.State}</state><contactNo>{addressRequestDto.PhoneNumber}</contactNo><loggedBy>{addressRequestDto.ApprovedBy}</loggedBy><email>{addressRequestDto.EmailAddress}</email><remarks>{addressRequestDto.Remarks}</remarks><landMark>{addressRequestDto.LandMark}</landMark><companyName>{addressRequestDto.CompanyName}</companyName><lastName>{addressRequestDto.LastName}</lastName><firstName>{addressRequestDto.FirstName}</firstName><branchId>{addressRequestDto.BranchId}</branchId><alias>{addressRequestDto.Alias}</alias><gender>{addressRequestDto.CustomerGender}</gender></otherRequestDetails>
                    ]]>
                                </body>
                                <submissionTime>{DateTime.Now:o}</submissionTime>
                            </soap:request>
                        </soapenv:Body>
                    </soapenv:Envelope>"
                ;
        }
    }
}
