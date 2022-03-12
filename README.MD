# Heatlh Check / Health Check UI


Project example using https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks

This project is separated into two services, the API is responsible to check a page health.
I have used two different methods to do the same, but the `AccountHealthCheck` have a personalised code to return randoly a error, just to test webhook notification.

In this project, the notification is sended to https://webhook.site/#!/8d00ea5d-6829-44c8-8e30-5258ab7e4398/ab1766f8-31b7-4d74-a29e-034fcbfd63fc/1

```cs
builder.Services.AddHttpClient<IHealthCheck, AccountHealthCheck>();
builder.Services.AddHealthChecks().AddUrlGroup(new Uri("https://ambev.com.br"),"Ambev",null,null,TimeSpan.FromSeconds(3)).AddCheck("Ambev Manual",new AccountHealthCheck());
```



