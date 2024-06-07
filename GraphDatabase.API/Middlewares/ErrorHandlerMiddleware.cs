namespace GraphDatabase.API.Middlewares;

/// <summary>
/// Class ErrorHandlerMiddleware.
/// </summary>
public class ErrorHandlerMiddleware
{
    /// <summary>
    /// The next
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorHandlerMiddleware"/> class.
    /// </summary>
    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the specified context.
    /// </summary>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var errorMessage = "There was an unhandled error while processing the request";
            _logger.LogError(exception, errorMessage);
            var response = context.Response;
            response.ContentType = "application/json";
            var body = new
                { message = errorMessage, exception = exception.GetBaseException()?.Message ?? errorMessage };
            response.StatusCode = StatusCodes.Status500InternalServerError;
            await response.WriteAsync(JsonSerializer.Serialize(body));
        }
    }
}