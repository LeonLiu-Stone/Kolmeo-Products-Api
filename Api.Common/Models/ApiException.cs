using System;

namespace Api.Common.Models
{
	/// <summary>
	/// a custom exception
	/// </summary>
	public class ApiException : Exception
	{
		public ApiException() { }

		public ApiException(string message) : base(message) { }

		public ApiException(string message, Exception innerException) : base(message, innerException) { }
	}
}
