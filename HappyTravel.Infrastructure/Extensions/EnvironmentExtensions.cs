using Microsoft.AspNetCore.Hosting;

namespace HappyTravel.Infrastructure.Extensions;

internal static class EnvironmentExtensions
{
    internal static bool IsLocal(this IWebHostEnvironment environment)
        => environment.EnvironmentName == "Local";
}