using HappyTravel.Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HappyTravel.Infrastructure.Extensions;

internal static class AuthenticationConfigurationExtensions
{
    internal static WebApplicationBuilder ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        var authorityOptions = builder.Configuration.GetSection("Authority").Get<AuthorityOptions>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.Authority = authorityOptions.AuthorityUrl;
            options.RequireHttpsMetadata = true;
            options.Audience = authorityOptions.Audience;
            options.AutomaticRefreshInterval = authorityOptions.AutomaticRefreshInterval;
        });

        return builder;
    }
}