using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Api.Common.Models;

namespace Api.Common.Middlewares
{
	/// <summary>
	/// a custom middleware for global error catch
	/// </summary>
	public class ErrorHandlerMiddleware
	{
		private readonly ILogger _logger;
		private readonly RequestDelegate _next;

		public ErrorHandlerMiddleware(
			RequestDelegate next
			, ILogger<ErrorHandlerMiddleware> logger)
		{
			_logger = logger;
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (ApiException ex)
			{
				//when catch a custom exception, no need to log again
				await HandleExceptionAsync(httpContext, ex);
			}
			catch (Exception ex)
			{
				//process unknown errors
				_logger.LogError(ex, $"Unknown exception: {ex.Message}:{ex.StackTrace}");
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			//custom respnose message
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			await context.Response.WriteAsync(new ApiErrorDetails()
			{
				StatusCode = context.Response.StatusCode,
				Message = $"Internal Server Error: {exception.Message}"
			}.ToString());
		}
	}
}
