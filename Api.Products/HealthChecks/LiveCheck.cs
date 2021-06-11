using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Api.Products.HealthChecks
{
	public class LiveCheck : IHealthCheck
	{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default(CancellationToken))
    {
			return Task.FromResult(
						HealthCheckResult.Healthy("The products api is working."));

			//return Task.FromResult(
			// new HealthCheckResult(context.Registration.FailureStatus,
			// "An unhealthy result."));
		}
  }
}
