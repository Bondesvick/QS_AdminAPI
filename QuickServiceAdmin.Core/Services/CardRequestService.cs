using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickServiceAdmin.Core.Helpers;
using QuickServiceAdmin.Core.Interface;
using QuickServiceAdmin.Core.Model;

namespace QuickServiceAdmin.Core.Services
{
    [ExcludeFromCodeCoverage]
    public class CardRequestService : ICardRequestService
    {
        private const string CardRequestAuthTokenCacheName = "CardRequestAuthToken";
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CardRequestService> _logger;
        private readonly IJsonRequestHelper _jsonRequestHelper;

        public CardRequestService(ILogger<CardRequestService> logger, HttpClient httpClient, IDistributedCache cache, IJsonRequestHelper jsonRequestHelper,
            IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _cache = cache;
            _configuration = configuration;
            _jsonRequestHelper = jsonRequestHelper;
        }

        public async Task RequestCard(CardRequestParams cardRequestParams)
        {
            // return;
            var jsonRequestString = JsonConvert.SerializeObject(cardRequestParams);

            _logger.LogInformation(jsonRequestString);

            var request = new StringContent(jsonRequestString, Encoding.UTF8, "application/json");

            var authToken = await GetAuthToken();

            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + authToken);
            _httpClient.DefaultRequestHeaders.Add("Channel",
                _configuration["AppSettings:CardRequestServiceConfig:Channel"]);
            _httpClient.DefaultRequestHeaders.Add("AppName",
                _configuration["AppSettings:CardRequestServiceConfig:AppName"]);
            _httpClient.DefaultRequestHeaders.Add("AppPass",
                _configuration["AppSettings:CardRequestServiceConfig:AppPass"]);

            var response =
                await _jsonRequestHelper.MakeJsonRequest("POST", "api/debitcard/logdebitcardrequest", _httpClient,
                    request);
            
            
            response = response.Replace("&lt;","<").Replace("&gt;", ">")
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

            if (responseCode == "000") return;

            var description = RequestHelper.GetFirstTagValue(response, "responseDescription");
            var detail = RequestHelper.GetFirstTagValue(response, "detail");

            var message = detail;

            if (string.IsNullOrEmpty(detail))
            {
                message = description;
            }

            throw new CustomErrorException(message, ResponseCodeConstants.BadRequest);
        }

        public async Task<CardRequestDetailsResponse> GetCardRequestDetails(string accountNumber)
        {
            var request = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            var currencyTask =
                _jsonRequestHelper.MakeJsonRequest("GET", "api/carddictionary/getcurrency", _httpClient, request);
            var cityTask = _jsonRequestHelper.MakeJsonRequest("GET", "api/carddictionary/getcity", _httpClient, request);
            var genderTask =
                _jsonRequestHelper.MakeJsonRequest("GET", "api/carddictionary/getgender", _httpClient, request);
            var titleTask =
                _jsonRequestHelper.MakeJsonRequest("GET", "api/carddictionary/gettitle", _httpClient, request);
            var maritalStatusTask =
                _jsonRequestHelper.MakeJsonRequest("GET", "api/carddictionary/getmaritalstatus", _httpClient, request);
            var branchesTask =
                _jsonRequestHelper.MakeJsonRequest("GET", "api/carddictionary/getbranches", _httpClient, request);
            var eligibleCardsTask = _jsonRequestHelper.MakeJsonRequest("POST", "api/carddictionary/geteligiblecards",
                _httpClient, new StringContent(JsonConvert.SerializeObject(new
                {
                    account_no = accountNumber
                }), Encoding.UTF8, "application/json"));

            var results = await Task.WhenAll(currencyTask, cityTask, genderTask, titleTask, maritalStatusTask,
                branchesTask, eligibleCardsTask);

            var resultClasses = results.Select(JsonConvert.DeserializeObject<JObject>);

            var resultClassesList = resultClasses.ToList();
            if (resultClassesList.Any(result => (string)result.SelectToken("responseCode") != "00"))
            {
                _logger.LogError("Could not get response " + string.Join(" -------------------- ", results));

               var resultClass = resultClassesList.FirstOrDefault(result => (string)result.SelectToken("responseCode") == "99");

                if (resultClass != null) {
                    throw new CustomErrorException((string)resultClass.SelectToken("responseDescription"), ResponseCodeConstants.Failure);
                }

                throw new Exception("Something went wrong while getting response for card request details");
            }

            var responseClasses = resultClassesList
                .Select(result => result.SelectToken("data")).ToList();

            var response = new CardRequestDetailsResponse
            {
                Currencies = responseClasses[0],
                Cities = responseClasses[1],
                Genders = responseClasses[2],
                Titles = responseClasses[3],
                MaritalStatuses = responseClasses[4],
                Branches = responseClasses[5],
                EligibleCards = responseClasses[6]
            };

            return response;
        }

        private async Task<string> GetAuthToken()
        {
            // Check if content exists in cache
            var cardRequestAuthToken = await _cache.GetStringAsync(CardRequestAuthTokenCacheName);
            if (cardRequestAuthToken != null) return cardRequestAuthToken;

            var jsonRequestString = JsonConvert.SerializeObject(new
            {
                sapId = _configuration["AppSettings:CardRequestServiceConfig:SapId"],
                password = _configuration["AppSettings:CardRequestServiceConfig:Password"],
                appId = _configuration["AppSettings:CardRequestServiceConfig:AppId"]
            });

            _logger.LogInformation(jsonRequestString);

            var request = new StringContent(jsonRequestString, Encoding.UTF8, "application/json");

            var response = await _jsonRequestHelper.MakeJsonRequest("POST", "api/authentication", _httpClient, request);

            var responseClass = JsonConvert.DeserializeObject<JObject>(response);

            if ((string) responseClass.SelectToken("responseCode") != "00")
                throw new Exception("Could not get auth token " + response);

            var token = (string) responseClass.SelectToken("data.token");
            var tokenExpiry = (DateTimeOffset?) responseClass.SelectToken("data.tokenExpiry");

            if (string.IsNullOrEmpty(token)) throw new Exception("Could not get auth token " + response);

            await _cache.SetStringAsync(CardRequestAuthTokenCacheName, token,
                new DistributedCacheEntryOptions {AbsoluteExpiration = tokenExpiry});

            return token;
        }
    }
}
