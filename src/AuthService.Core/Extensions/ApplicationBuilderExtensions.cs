namespace AuthService.Core.Extensions;

<<<<<<< HEAD
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
=======
public class ApplicationBuilderExtensions
{
    
>>>>>>> 65d69c9ca735abdf7fd91f28c094b5514ed5658d
}