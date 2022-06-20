using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace HappyTravel.Infrastructure.Extensions;

internal static class LoggerConfigurationExtensions
{
    internal static WebApplicationBuilder ConfigureLogger(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureLogging((_, logging) =>
        {
            logging.Configure(options =>
            {
                options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId
                                                  | ActivityTrackingOptions.TraceId
                                                  | ActivityTrackingOptions.ParentId
                                                  | ActivityTrackingOptions.Baggage
                                                  | ActivityTrackingOptions.Tags;
            });
        });

        return builder;
    }
}