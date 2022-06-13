using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;

namespace HappyTravel.Infrastructure.Extensions;

internal static class KestrelConfigurationExtensions
{
    internal static WebApplicationBuilder ConfigureKestrel(this WebApplicationBuilder builder)
    {
        builder.WebHost.UseKestrel(options =>
        {
            options.Listen(IPAddress.Any, builder.Configuration.GetValue<int>("HTDC_WEBAPI_PORT"));
            options.Listen(IPAddress.Any, builder.Configuration.GetValue<int>("HTDC_METRICS_PORT"));
            options.Listen(IPAddress.Any, builder.Configuration.GetValue<int>("HTDC_HEALTH_PORT"));
            options.Listen(IPAddress.Any, builder.Configuration.GetValue<int>("HTDC_GRPC_PORT"), o =>
            {
                o.Protocols = HttpProtocols.Http2;
            });
        });

        return builder;
    }
}