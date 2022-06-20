using System.Text.Json;
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

            if (builder.Environment.IsLocal())
            {
                logging.AddSimpleConsole(options =>
                {
                    options.SingleLine = false;
                    options.IncludeScopes = true;
                    options.UseUtcTimestamp = false;
                });
            }
            else
            {
                logging.AddJsonConsole(options =>
                {
                    options.IncludeScopes = true;
                    options.UseUtcTimestamp = true;
                    options.JsonWriterOptions = new JsonWriterOptions
                    {
                        Indented = false
                    };
                });
            }
        });

        return builder;
    }
}