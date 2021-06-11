using System;
using Newtonsoft.Json;

namespace Api.Common.Models
{
	/// <summary>
	/// a common error model in API
	/// </summary>
	public class ApiErrorDetails
	{
		/// <summary>
		/// should be samne as http status ciode
		/// </summary>
		public int StatusCode { get; set; }

		/// <summary>
		/// error details
		/// </summary>
		public string Message { get; set; }


		/// <summary>
		/// format the error details
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
