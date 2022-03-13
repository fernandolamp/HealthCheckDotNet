using HealthCheckApi.Services;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient<IHealthCheck, GoogleHealthCheck>();
var uri = builder.Configuration.GetSection("UriCheck").Value;
builder.Services.AddHealthChecks().AddUrlGroup(new Uri(uri), "Google Check", null, null, TimeSpan.FromSeconds(3))
    .AddCheck("Google Manual", new GoogleHealthCheck());

var app = builder.Build();


app.UseHealthChecks("/hc", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json; charset=utf-8";
        var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(report));
        await context.Response.Body.WriteAsync(bytes);
    }
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
