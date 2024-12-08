using EcomPulse.Service;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomControllerBase : ControllerBase
    {
        [NonAction]
        public IActionResult CreateObjectResult<T>(ServiceResult<T> result)
        {
            if (result.IsError)
            {
                if (result is null)
                {
                    return NotFound();
                }

                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "An error occurred",
                    Status = (int)result.HttpStatus,
                    Detail = result.Errors.First(),
                };
                return new ObjectResult(problemDetails)
                {
                    StatusCode = problemDetails.Status,
                };
            }
            return new ObjectResult(result.Data)
            {
                StatusCode = (int)result.HttpStatus,
            };
        }
        [NonAction]
        public IActionResult CreateObjectResult(ServiceResult result)
        {
            if (result.IsError)
            {
                if (result is null)
                {
                    return NotFound();
                }

                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "An error occurred",
                    Status = (int)result.HttpStatus,
                    Detail = result.Errors.First(),
                };
                return new ObjectResult(problemDetails)
                {
                    StatusCode = problemDetails.Status,
                };
            }
            return NoContent();
        }
    }
}
