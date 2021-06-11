using System;
using Microsoft.Extensions.Logging;

using Moq;

namespace Api.Common.Test
{
	public class StubHelper
	{
		public static Mock<ILogger<T>> StubILogger<T>() where T : class => new Mock<ILogger<T>>();

		/// <summary>
		/// a common way to verity logs
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="logger">logger instance</param>
		/// <param name="level">log level</param>
		/// <param name="message">expected message</param>
		public static void VerifyLog<T>(Mock<ILogger<T>> logger, LogLevel level, string message)
		{
			logger.Verify(x => x.Log(
				level,
				It.IsAny<EventId>(),
				It.Is<object>(o => o.ToString().Contains(message)),
				It.IsAny<Exception>(),
				It.IsAny<Func<object, Exception, string>>()),
				Times.Once);
		}
	}
}
