using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickServiceAdmin.Core.Model;

namespace QuickService_AdminAPI.Controllers
{
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public ActionResult<GenericApiResponse> Index()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            _logger.LogError(context.Error.Message);

            if (!(context.Error is CustomErrorException exception))
            {
                return Ok(new GenericApiResponse
                {
                    ResponseCode = ResponseCodeConstants.InternalException,
                    ResponseDescription = "Something went wrong"
                });
            }

            return Ok(new GenericApiResponse
            {
                ResponseCode = exception.StatusCode,
                ResponseDescription = exception.Message
            });
        }
    }
}
