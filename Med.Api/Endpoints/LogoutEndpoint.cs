using Med.Api.Common;
using Microsoft.AspNetCore.Identity;

namespace Med.Api.Endpoints
{
    public class LogoutEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app
            .MapPost("/logout", HandleAsync)
            .RequireAuthorization();

        private static async Task<IResult> HandleAsync(SignInManager<IdentityUser<long>> signInManager)
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        }
    }
}
