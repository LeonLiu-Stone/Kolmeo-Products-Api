
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using Api.Common.Middlewares;
using Api.Products.HealthChecks;
using Api.Products.Services;
using Api.Products.Data;

namespace Api.Products
{
	/// <summary>
	/// NB: Should create a basic startup to handle some common things,
	/// but not implemented in this code test
	/// </summary>
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<KolmeoDataContext>(options =>
				options.UseSqlite(@"Data Source=/tmp/kolmeo.db"));

			services.AddTransient<IProductProvider, ProductProvider>();

			services.AddControllers();
			services.AddHealthChecks();

			services.AddApiVersioning(options => {
				options.ReportApiVersions = true;
			});
			services.AddVersionedApiExplorer(options => {
				options.GroupNameFormat = "'v'VVV";
				options.SubstituteApiVersionInUrl = true;
			});

			services.AddSwaggerGen();

			services.AddHealthChecks()
				.AddCheck<LiveCheck>(
						"working_health_check",
						failureStatus: HealthStatus.Degraded,
						tags: new[] { "working" });
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "V 1.0");
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHealthChecks("/health");
			});


			app.ConfigureCustomMiddleware();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
