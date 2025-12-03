namespace AuthService.Core.Extensions;

using Microsoft.AspNetCore.Builder;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseAuthServicePipeline(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseExceptionHandler("/error");
        app.UseStatusCodePages();
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}