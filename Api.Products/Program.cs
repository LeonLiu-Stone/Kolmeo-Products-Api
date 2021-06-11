using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Api.Products
{
	public class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex) {
				Console.WriteLine($"An unknown error: {ex.Message}");
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
				Host.CreateDefaultBuilder(args)
						.ConfigureLogging(logging =>
						{
							logging.AddConsole();
						})
						.ConfigureWebHostDefaults(webBuilder =>
						{
							webBuilder.UseStartup<Startup>();
						});
	}
}
