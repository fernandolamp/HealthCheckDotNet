using HealthChecks.UI.Core.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecksUI().AddSqliteStorage(builder.Configuration.GetConnectionString("Sqlite"));
builder.Services.AddDbContext<HealthChecksDb>(options =>
{
    options.UseSqlite();
});


var app = builder.Build();

app.UseHttpsRedirection();
app.UseHealthChecksUI(config => {
    config.UIPath = "/hc-ui";
});

//app.MapControllers();
app.MapHealthChecksUI();

app.Run();
