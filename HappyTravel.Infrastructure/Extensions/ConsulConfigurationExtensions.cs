using HappyTravel.ConsulKeyValueClient.ConfigurationProvider.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace HappyTravel.Infrastructure.Extensions;

internal static class ConsulConfigurationExtensions
{
    internal static WebApplicationBuilder ConfigureConsul(this WebApplicationBuilder builder, string key)
    {
        var consulHttpAddr = builder.Configuration.GetValue<string>("CONSUL_HTTP_ADDR");
        var consulHttpToken = builder.Configuration.GetValue<string>("CONSUL_HTTP_TOKEN");

        builder.Configuration.AddConsulKeyValueClient(url: consulHttpAddr, 
            key: "common", 
            token: consulHttpToken, 
            bucketName: builder.Environment.EnvironmentName, 
            optional: builder.Environment.IsLocal());

        builder.Configuration.AddConsulKeyValueClient(url: consulHttpAddr, 
            key: key, 
            token: consulHttpToken, 
            bucketName: builder.Environment.EnvironmentName, 
            optional: builder.Environment.IsLocal());

        return builder;
    }
}