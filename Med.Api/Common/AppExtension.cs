using Microsoft.AspNetCore.Identity;

namespace Med.Api.Common;

public static class AppExtension
{
    public static void ConfigureDevEnvironment(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapSwagger().RequireAuthorization();
    }

    public static void UseSecurity(this WebApplication app)
    {

        app.UseAuthentication();
        app.UseAuthorization();

    }
}
