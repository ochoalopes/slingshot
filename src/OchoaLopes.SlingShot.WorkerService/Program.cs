using Microsoft.EntityFrameworkCore;
using OchoaLopes.SlingShot.Infra.Context;
using OchoaLopes.SlingShot.Infra.HostedService;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
        config.AddCommandLine(args);
    })
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        services.AddHostedService<MessageProcessingHostedService>();
        services.AddApplicationInsightsTelemetry();
        services.AddDbContext<SlingShotContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("SlingShotConnectionString")));
    });

var host = builder.Build();

await host.RunAsync();
