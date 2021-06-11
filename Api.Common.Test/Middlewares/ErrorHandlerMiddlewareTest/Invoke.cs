using System;
using System.Threading.Tasks;
using Xunit;

using Api.Common.Middlewares;
using Api.Common.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Api.Common.Test.Middlewares.ErrorHandlerMiddlewareTest
{
	public class Invoke
	{
			[Fact]
			public async Task WhenACustomExceptionIsRaised_StatusInternalServerErrorShouldBeReturned()
			{
				// Arrange
				var middleware = new ErrorHandlerMiddleware((innerHttpContext) => {
					throw new ApiException("A test custom exception");
				}, StubHelper.StubILogger<ErrorHandlerMiddleware>().Object);

				var context = new DefaultHttpContext();
				context.Response.Body = new MemoryStream();

				//Act
				await middleware.Invoke(context);

				context.Response.Body.Seek(0, SeekOrigin.Begin);
				var reader = new StreamReader(context.Response.Body);
				var streamText = reader.ReadToEnd();
				var objResponse = JsonConvert.DeserializeObject<ApiErrorDetails>(streamText);

				//Assert
				Assert.Contains("A test custom exception", objResponse.Message);
				Assert.Equal(context.Response.StatusCode, (int)HttpStatusCode.InternalServerError);
			}

			[Fact]
			public async Task WhenAUnKnownExceptionIsRaised_StatusInternalServerErrorShouldBeReturned()
			{
				// Arrange
				var middleware = new ErrorHandlerMiddleware((innerHttpContext) => {
					throw new Exception("A test exception");
				}, StubHelper.StubILogger<ErrorHandlerMiddleware>().Object);

				var context = new DefaultHttpContext();
				context.Response.Body = new MemoryStream();

				//Act
				await middleware.Invoke(context);

				context.Response.Body.Seek(0, SeekOrigin.Begin);
				var reader = new StreamReader(context.Response.Body);
				var streamText = reader.ReadToEnd();
				var objResponse = JsonConvert.DeserializeObject<ApiErrorDetails>(streamText);

				//Assert
				Assert.Contains("A test exception", objResponse.Message);
				Assert.Equal(context.Response.StatusCode, (int)HttpStatusCode.InternalServerError);
			}
	
	}
}
