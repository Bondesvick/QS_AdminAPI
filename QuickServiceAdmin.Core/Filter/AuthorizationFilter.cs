using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using QuickServiceAdmin.Core.Model;

namespace QuickServiceAdmin.Core.Filter
{

    public class AuthorizationFilter : IAsyncActionFilter
    {
        private readonly IDistributedCache _redis;
        private readonly ILogger<AuthorizationFilter> _logger;
        private readonly IConfiguration _configuration;

        public AuthorizationFilter(IDistributedCache redis, ILogger<AuthorizationFilter> logger, IConfiguration configuration)
        {
            _logger = logger;
            _redis = redis;
            _configuration = configuration;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("Authorizing request");
            
            if (_configuration["AppSettings:SkipValidation"] == "1")
            {
                await next();
            }
            else
            {
                if (await ValidateAuthorizationToken(context))
                {
                    _logger.LogInformation("Request authorized");
                    // Execute the rest of the MVC filter pipeline
                    await next();
                }
                _logger.LogError("Request unauthorized");
            }
        }

        private async Task<bool> ValidateAuthorizationToken(ActionExecutingContext context)
        {
            var authorization =
                context.HttpContext.Request.Headers.SingleOrDefault(o => o.Key == AppConstants.Authorization);

            if (!context.ModelState.IsValid)
            {
                _logger.LogError("Authorization Filter - Model State is not valid");
                context.Result = new BadRequestObjectResult(new GenericApiResponse
                {
                    ResponseCode = ResponseCodeConstants.BadRequest,
                    ResponseDescription = "Payload is invalid"
                });
            }
            else
            {
                //validate custom headers here.
                var isTokenValid = await IsTokenValid(authorization.Value);
                if (isTokenValid) return await Task.FromResult(true);
                _logger.LogError("Authorization Filter - Token is not valid");

                context.Result = new UnauthorizedObjectResult(new GenericApiResponse
                {
                    ResponseCode = ResponseCodeConstants.Failure,
                    ResponseDescription = ResponseDescription.AuthFailure,
                });
            }

            return await Task.FromResult(false);
        }

        private async Task<bool> IsTokenValid(string authorization)
        {
            Guard.ArgumentNotNullOrEmpty(authorization, "Authorization token cannot be null");

            var authArray = authorization.Split(" ");
            var authCode = authArray[^1];

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(authCode) as JwtSecurityToken;

            var claims = jsonToken?.Claims.ToList() ?? throw new Exception("Failure to read token: JSON token is null");

            var claim = claims.First(c => c.Type.Equals("unique_name", StringComparison.Ordinal)).Value;
            //  string flags = await _redisCacheService.SetItemAsync($"Pay_Mgr_Auth_{userId}", authToken);

            var claimDetail = await _redis.GetStringAsync($"BackOffice_Auth_{claim}");

            var userAuth = await _redis.GetStringAsync($"General_Service_Auth_{claim}");

            if (claimDetail == null || userAuth == null) return false;
            return userAuth == claimDetail;
        }
    }
}
