using System.Net;
using me_faz_um_pix.Exceptions;

namespace me_faz_um_pix.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
  private readonly RequestDelegate _next = next;
  private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

  public async Task InvokeAsync(HttpContext context)
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
    _logger.LogError(exception, "An unexpected error occurred.");

    //More log stuff        

    ExceptionResponse response = exception switch
    {
      UnauthorizedException _ => new ExceptionResponse(HttpStatusCode.Unauthorized, exception.Message),
      NotFoundException _ => new ExceptionResponse(HttpStatusCode.NotFound, exception.Message),
      ForbiddenException _ => new ExceptionResponse(HttpStatusCode.Forbidden, exception.Message),
      ConflictException _ => new ExceptionResponse(HttpStatusCode.Conflict, exception.Message),
      RecentPaymentViolationException _ => new ExceptionResponse(HttpStatusCode.Conflict, exception.Message),
      _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
    };

    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)response.StatusCode;
    await context.Response.WriteAsJsonAsync(response);
  }
}

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);