using System;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuickServiceAdmin.Core.Interface;
using QuickServiceAdmin.Core.Model;

namespace QuickService_AdminAPI.Controllers
{
    [ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IAuthenticateService authenticateService, IConfiguration configuration)
        {
            _authenticateService = authenticateService;
            _configuration = configuration;
        }

        [Route("AuthenticateUser")]
        [HttpPost]
        public async Task<ActionResult<GenericApiResponse<TokenGenerationResponse>>> AuthenticateUser(
            [FromBody] LoginRequest request)
        {
            if (_configuration["AppSettings:SkipValidation"] == "1")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secret = _configuration.GetSection("Secret").ToString() ?? "";
                var key = Encoding.ASCII.GetBytes(secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, request.UserId) }),
                    Expires = DateTime.UtcNow.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                }; var token = tokenHandler.CreateToken(tokenDescriptor);
                var newToken = tokenHandler.WriteToken(token);

                return Ok(
                new GenericApiResponse<TokenGenerationResponse>
                {
                    ResponseCode = ResponseCode.Success,
                    ResponseDescription = ResponseDescription.Success,
                    Data = new TokenGenerationResponse
                    {
                        NewToken = newToken
                    }
                });
            }

            string jwtToken = Request.Headers.ContainsKey("Authorization")
                ? Request.Headers["Authorization"]
                : throw new CustomErrorException("Authorization key not set", ResponseCodeConstants.BadRequest);

            if (string.IsNullOrEmpty(jwtToken))
            {
                throw new CustomErrorException("JWT Token is empty", ResponseCodeConstants.BadRequest);
            }

            var tokenGenerationResponse = await _authenticateService.GenerateToken(request.UserId, jwtToken);

            return Ok(
                new GenericApiResponse<TokenGenerationResponse>
                {
                    ResponseCode = ResponseCode.Success,
                    ResponseDescription = ResponseDescription.Success,
                    Data = tokenGenerationResponse
                });
        }
    }
}