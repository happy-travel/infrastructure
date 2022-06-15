using HappyTravel.StdOutLogger.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace HappyTravel.Infrastructure.Extensions;

internal static class LoggerConfigurationExtensions
{
    internal static WebApplicationBuilder ConfigureLogger(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureLogging((hostingContext, logging) =>
        {
            logging.ClearProviders()
                .AddConfiguration(hostingContext.Configuration.GetSection("Logging"));

            var env = hostingContext.HostingEnvironment;

            if (env.IsLocal())
                logging.AddConsole();
            else
            {
                logging.AddStdOutLogger(setup =>
                {
                    setup.IncludeScopes = true;
                    setup.UseUtcTimestamp = true;
                });
                logging.AddSentry();
            }
        });

        return builder;
    }
}