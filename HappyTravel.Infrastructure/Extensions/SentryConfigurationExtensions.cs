using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace HappyTravel.Infrastructure.Extensions;

internal static class SentryConfigurationExtensions
{
    internal static WebApplicationBuilder ConfigureSentry(this WebApplicationBuilder builder)
    {
        builder.WebHost.UseSentry(options =>
        {
            options.Dsn = builder.Configuration.GetValue<string>("Sentry:Endpoint");
            options.Environment = builder.Environment.EnvironmentName;
            options.IncludeActivityData = true;
            options.BeforeSend = sentryEvent =>
            {
                if (Activity.Current is null)
                    return sentryEvent;

                foreach (var (key, value) in Activity.Current.Baggage)
                    sentryEvent.SetTag(key, value ?? string.Empty);

                sentryEvent.SetTag("TraceId", Activity.Current.TraceId.ToString());
                sentryEvent.SetTag("SpanId", Activity.Current.SpanId.ToString());

                return sentryEvent;
            };
        });

        return builder;
    }
}