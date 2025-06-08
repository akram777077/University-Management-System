using Applications.Shared;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.ResultExtension
{
    public static class ResultExtensions
    {
        public static ActionResult HandleResult(this Result result)
        {
            return result.ErrorType switch
            {
                ErrorType.NotFound => new NotFoundObjectResult(result.Error),
                ErrorType.BadRequest => new BadRequestObjectResult(result.Error),
                ErrorType.Conflict => new ConflictObjectResult(result.Error),
                _ => new ObjectResult(result.Error) { StatusCode = 500 }
            };
        }
    }
}