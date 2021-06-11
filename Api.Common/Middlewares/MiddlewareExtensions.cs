using System;
using Microsoft.AspNetCore.Builder;

namespace Api.Common.Middlewares
{
	public static class MiddlewareExtensions
	{
		/// <summary>
		/// register custom middlewares
		/// </summary>
		/// <param name="app"></param>
		public static void ConfigureCustomMiddleware(this IApplicationBuilder app)
		{
			//Globle error handling
			app.UseMiddleware<ErrorHandlerMiddleware>();
		}
	}
}
