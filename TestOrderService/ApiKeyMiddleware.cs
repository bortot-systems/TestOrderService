using System.Net;

namespace TestOrderService
{
    public class ApiKeyMiddleware
    {
        private const string ApiKeyHeaderName = "X-API-Key";
        private readonly RequestDelegate _next;
        private readonly string _apiKey;

        public ApiKeyMiddleware(RequestDelegate next, string apiKey)
        {
            _next = next;
            _apiKey = apiKey;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Allow access to Swagger UI and JSON endpoints
            if (context.Request.Path.StartsWithSegments("/swagger") ||
                context.Request.Path.StartsWithSegments("/v1/swagger.json"))
            {
                await _next(context);
                return;
            }

            // Check for API key in other requests
            if (!context.Request.Headers.TryGetValue(ApiKeyHeaderName, out var extractedApiKey) ||
                !string.Equals(extractedApiKey, _apiKey, StringComparison.Ordinal))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
                await context.Response.WriteAsync("Unauthorized request");
                return;
            }

            await _next(context);
        }
    }

}
