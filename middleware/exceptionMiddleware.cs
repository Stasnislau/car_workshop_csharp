using System.Net;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var errorResponse = new ErrorResponse
        {
            Success = false,
            Message = "Internal Server Error"
        };
        int statusCode = (int)HttpStatusCode.InternalServerError;
        if (exception is CustomException)
        {
            errorResponse.Message = exception.Message;
            statusCode = (exception as CustomException).StatusCode;
        }
        context.Response.StatusCode = statusCode;
        Console.WriteLine(exception.Message);
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}

public class ErrorResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }
}
