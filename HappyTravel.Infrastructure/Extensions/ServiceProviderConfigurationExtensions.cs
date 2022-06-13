using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace HappyTravel.Infrastructure.Extensions;

internal static class ServiceProviderConfigurationExtensions
{
    internal static WebApplicationBuilder ConfigureServiceProvider(this WebApplicationBuilder builder)
    {
        builder.WebHost.UseDefaultServiceProvider(options => 
        {
            options.ValidateOnBuild = true;
            options.ValidateScopes = true;
        });

        return builder;
    }
}