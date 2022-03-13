using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace HealthCheckApi.Services;
public class GoogleHealthCheck : IHealthCheck
{

    public GoogleHealthCheck()
    {

    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {

        using (HttpClient client = new HttpClient())
        {
            var uri = "https://google.com";
            //test error webhook
            if (DateTime.UtcNow.Minute % 2 == 0)
            {
                uri = "http://httpbin.org/status/500";
            }

            var result = await client.GetAsync(uri);


            if (result.IsSuccessStatusCode)
            {
                return HealthCheckResult.Healthy();
            }

            return new HealthCheckResult(status: context.Registration.FailureStatus, result.ReasonPhrase);

        }


    }
}
