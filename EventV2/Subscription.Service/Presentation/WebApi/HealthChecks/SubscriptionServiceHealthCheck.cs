using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Subscription.WebApi.HealthChecks;

public class SubscriptionServiceHealthCheck : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        return await Task.FromResult(HealthCheckResult.Healthy("Subscription service is working"));
    }
}