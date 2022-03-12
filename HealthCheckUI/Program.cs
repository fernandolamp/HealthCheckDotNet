using HealthChecks.UI.Core.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();
builder.Services.AddHealthChecksUI();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<HealthChecksDb>(options =>
{    
    options.UseInMemoryDatabase("uihealth");
});


var app = builder.Build();

app.UseHttpsRedirection();
app.UseHealthChecksUI(config => config.UIPath = "/hc-ui");

app.MapControllers();
app.MapHealthChecksUI();

app.Run();
