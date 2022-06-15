using System;
using HappyTravel.Infrastructure.Models;
using Microsoft.AspNetCore.Builder;

namespace HappyTravel.Infrastructure.Extensions;

public static class InfrastructureConfigurationExtensions
{
    public static WebApplicationBuilder ConfigureInfrastructure(this WebApplicationBuilder builder, Action<InfrastructureOptions>? action)
    {
        ArgumentNullException.ThrowIfNull(action);
        
        var options = new InfrastructureOptions();
        action.Invoke(options);

        return builder.AddConfiguration()
            .ConfigureConsul(options.ConsulKey)
            .ConfigureKestrel()
            .ConfigureSentry()
            .ConfigureLogger()
            .ConfigureServiceProvider()
            .ConfigureTelemetry()
            .ConfigureAuthentication();
    }
}