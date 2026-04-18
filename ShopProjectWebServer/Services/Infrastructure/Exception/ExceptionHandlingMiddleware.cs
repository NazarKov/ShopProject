using ShopProjectWebServer.Services.Infrastructure.Logging.Interface;
using System.Text.Json;

namespace ShopProjectWebServer.Services.Infrastructure.Exception
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILoggerService _loggerService;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next,ILoggerService loggerService, IWebHostEnvironment env)
        {
            _loggerService = loggerService;
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (System.Exception ex)
            {
                _loggerService.WriteLog("[Data:"+DateTime.Now+"] "+"[Where]"+ex.StackTrace +"\n[Error] "+ex.Message);

                if (context.Request.Path.StartsWithSegments("/Api"))
                {
                    context.Response.ContentType = "application/json";

                    var statusCode = ex switch
                    {
                        ArgumentException => StatusCodes.Status400BadRequest,
                        UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                        KeyNotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    context.Response.StatusCode = statusCode;

                    var result = JsonSerializer.Serialize(new
                    {
                        error = ex.Message
                    });

                    await context.Response.WriteAsync(result);
                }

                else
                { 
                    throw;
                }
            }
        } 
    }
}
