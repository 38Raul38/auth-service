namespace AuthService.Core.Extensions;

using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddAuthServiceCore(this IServiceCollection services, Action<JwtBearerOptions>? configureJwt = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var builder = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        builder.AddJwtBearer(options =>
        {
            configureJwt?.Invoke(options);
        });

        services.AddAuthorization();

        return services;
    }
}