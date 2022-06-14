using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace HappyTravel.Infrastructure.Extensions;

internal static class ConfigurationExtensions
{
    internal static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile(path: "appsettings.json", 
            optional: false, 
            reloadOnChange: true);
        
        builder.Configuration.AddJsonFile(path: $"appsettings.{builder.Environment.EnvironmentName}.json", 
            optional: true, 
            reloadOnChange: true);

        builder.Configuration.AddEnvironmentVariables();

        return builder;
    }
}