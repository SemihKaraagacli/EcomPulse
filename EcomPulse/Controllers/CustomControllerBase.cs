using EcomPulse.Service;
using Microsoft.AspNetCore.Mvc;


namespace EcomPulse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomControllerBase : ControllerBase
    {
        private readonly ILogger<CustomControllerBase> _logger;
        public CustomControllerBase(ILogger<CustomControllerBase> logger)
        {
            _logger = logger;
        }
        [NonAction]
        public IActionResult CreateObjectResult<T>(ServiceResult<T> result)
        {
            if (result.IsError)
            {
                if (result is null)
                {
                    _logger.LogError($"Message: Result is null | Returning status: {result.HttpStatus}");
                    return NotFound();
                }

                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "An error occurred",
                    Status = (int)result.HttpStatus,
                    Detail = result.Errors!.First(),
                };

                _logger.LogError($"Error occurred: {problemDetails.Detail} | Error status:{problemDetails.Status}");

                return new ObjectResult(problemDetails)
                {
                    StatusCode = problemDetails.Status,
                };
            }

            _logger.LogInformation($"Message: Data returned | Returning status: {result.HttpStatus}");
            return new ObjectResult(result.Data)
            {
                StatusCode = (int)result.HttpStatus,
            };
        }

        [NonAction]
        public IActionResult CreateObjectResult(ServiceResult result)
        {
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString();
            var actionName = ControllerContext.RouteData.Values["action"].ToString();

            if (result.IsError)
            {
                if (result is null)
                {
                    _logger.LogError($"Message: Result is null | Returning status: {result.HttpStatus}");
                    return NotFound();
                }

                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "An error occurred",
                    Status = (int)result.HttpStatus,
                    Detail = result.Errors!.First(),
                };

                _logger.LogError($"Error occurred: {problemDetails.Detail} | Error status:{problemDetails.Status}");

                return new ObjectResult(problemDetails)
                {
                    StatusCode = problemDetails.Status,
                };
            }

            _logger.LogInformation($"Message: Data returned | Returning status: {result.HttpStatus}");
            return NoContent();
        }
    }
}
