using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;

namespace HealthCheckApi.Services
{
    public class AccountHealthCheck : IHealthCheck
    {        

        public AccountHealthCheck()
        {            

        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {

            using (HttpClient client = new HttpClient())
            {
                var uri = "https://www.ambev.com.br";
                //test error webhook
                if (DateTime.UtcNow.Minute % 2 == 0)
                {
                    uri = "https://www.ambev.com.br/asdfasdfa";
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
}
