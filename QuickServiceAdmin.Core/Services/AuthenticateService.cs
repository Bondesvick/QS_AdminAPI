using System;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using QuickServiceAdmin.Core.Interface;
using QuickServiceAdmin.Core.Model;

namespace QuickServiceAdmin.Core.Services
{
    [ExcludeFromCodeCoverage]
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IConfiguration _configuration;
        private readonly IDistributedCache _redis;
        private readonly IJsonRequestHelper _jsonRequestHelper;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthenticateService(IJsonRequestHelper jsonRequestHelper, IDistributedCache redis,
            IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _jsonRequestHelper = jsonRequestHelper;
            _redis = redis;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TokenGenerationResponse> GenerateToken(string userId, string jwtToken)
        {
            var authToken = await ValidateAuthenticationToken(userId, jwtToken);
            var tokenGenerationResponse = new TokenGenerationResponse {NewToken = null};

            if (string.IsNullOrEmpty(authToken)) throw new Exception("Auth token is empty");

            await _redis.SetStringAsync($"General_Service_Auth_{userId}", authToken);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();

            //var key = Encoding.ASCII.GetBytes(appID);
            var secret = _configuration.GetSection("Secret").ToString() ?? "";
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Name, userId)}),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var newToken = tokenHandler.WriteToken(token);

            tokenGenerationResponse.NewToken = newToken;

            return tokenGenerationResponse;
        }

        public async Task<string> ValidateAuthenticationToken(string userId, string jwtToken)
        {
            var request = new StringContent($"\"{userId}\"", Encoding.UTF8, "application/json");

            // Pass the handler to httpclient(from you are calling api)
            var httpClient = _httpClientFactory.CreateClient("AuthClient");
            
            httpClient.BaseAddress = new Uri(AppSettings.ValidateToken);
            httpClient.DefaultRequestHeaders.Add("Authorization",jwtToken);
            var response = await _jsonRequestHelper.MakeJsonRequest("POST", string.Empty, httpClient, request);

            var authResponse = JsonConvert.DeserializeObject<ValidateAuthTokenResponse>(response);
            if (authResponse == null) throw new Exception("Auth Response Class is null: " + response);

            return authResponse.Head.ResponseCode == "00" ? authResponse.Body.ToString() : "";
        }
    }
}
