using FCI.BookCave.APIs.Models;
using FCI.BookCave.Domain.Exception;
using FCI.BookCave.Domain.Exceptions;

namespace FCI.BookCave.APIs.Middlewares
{
	public class CustomExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<CustomExceptionHandlingMiddleware> _logger;

		public CustomExceptionHandlingMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

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
			var (statusCode, errorMessage) = exception switch
			{
				NotFoundException => (StatusCodes.Status404NotFound, exception.Message),
				BadRequestException => (StatusCodes.Status400BadRequest, exception.Message),
				_ => (StatusCodes.Status500InternalServerError, exception.StackTrace ?? "An unexpected error occurred.")
			};

			if (statusCode == StatusCodes.Status500InternalServerError)
			{
				_logger.LogError(exception, "Unhandled exception occurred.");
			}

			var errorResponse = new ApiErrorResponse(statusCode, errorMessage);

			context.Response.StatusCode = statusCode;
			context.Response.ContentType = "application/json";

			await context.Response.WriteAsJsonAsync(errorResponse);
		}
	}
}
