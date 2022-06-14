using HappyTravel.Telemetry.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace HappyTravel.Infrastructure.Extensions;

internal static class TelemetryConfigurationExtensions
{
    internal static WebApplicationBuilder ConfigureTelemetry(this WebApplicationBuilder builder)
    {
        builder.Services.AddTracing(builder.Configuration, options =>
        {
            options.ServiceName = $"{builder.Environment.ApplicationName}-{builder.Environment.EnvironmentName}";
            options.RedisEndpoint = builder.Configuration.GetValue<string>(builder.Configuration.GetValue<string>("Redis:Endpoint"));
            options.JaegerHost = builder.Environment.IsLocal()
                ? builder.Configuration.GetValue<string>("Jaeger:AgentHost")
                : builder.Configuration.GetValue<string>(builder.Configuration.GetValue<string>("Jaeger:AgentHost"));
            options.JaegerPort = builder.Environment.IsLocal()
                ? builder.Configuration.GetValue<int>("Jaeger:AgentPort")
                : builder.Configuration.GetValue<int>(builder.Configuration.GetValue<string>("Jaeger:AgentPort"));
        });

        return builder;
    }
}