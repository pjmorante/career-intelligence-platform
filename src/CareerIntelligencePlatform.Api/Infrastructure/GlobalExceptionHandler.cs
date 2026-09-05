using CareerIntelligencePlatform.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CareerIntelligencePlatform.Api.Infrastructure.ExceptionHandling;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
  private readonly ILogger<GlobalExceptionHandler> _logger;

  public GlobalExceptionHandler(
      ILogger<GlobalExceptionHandler> logger)
  {
    _logger = logger;
  }

  public async ValueTask<bool> TryHandleAsync(
      HttpContext httpContext,
      Exception exception,
      CancellationToken cancellationToken)
  {
    _logger.LogError(
        exception,
        "An unhandled exception occurred.");

    if (exception is ValidationException validationException)
    {
      var errors = validationException.Errors
          .GroupBy(error => error.PropertyName)
          .ToDictionary(
              group => group.Key,
              group => group
                  .Select(error => error.ErrorMessage)
                  .ToArray());

      var problemDetails = new ValidationProblemDetails(errors)
      {
        Status = StatusCodes.Status400BadRequest,
        Title = "Validation failed.",
        Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
      };

      httpContext.Response.StatusCode =
          StatusCodes.Status400BadRequest;

      await httpContext.Response.WriteAsJsonAsync(
          problemDetails,
          cancellationToken);

      return true;
    }

    if (exception is DomainException domainException)
    {
      var problemDetails = new ProblemDetails
      {
        Status = StatusCodes.Status400BadRequest,
        Title = "Domain validation failed.",
        Detail = domainException.Message,
        Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1"
      };

      problemDetails.Extensions["code"] = domainException.Code;

      httpContext.Response.StatusCode =
          StatusCodes.Status400BadRequest;

      await httpContext.Response.WriteAsJsonAsync(
          problemDetails,
          cancellationToken);

      return true;
    }

    return false;
  }
}