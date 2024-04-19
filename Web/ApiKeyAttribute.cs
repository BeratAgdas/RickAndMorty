using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace Web;

public class ApiKeyAttribute : ActionFilterAttribute
{
    private const string ApiKeyHeader = "X-Api-Key";
    private const string ExpectedApiKey = "api-key"; // Beklenen API anahtarı

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeader, out StringValues extractedApiKey))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (ExpectedApiKey.Equals(extractedApiKey))
        {
            base.OnActionExecuting(context);
            return;
        }

        context.Result = new UnauthorizedResult();
    }
}
