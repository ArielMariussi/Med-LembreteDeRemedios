using Med.Api.Common;
using Med.Api.Endpoints.Medicines;
using Microsoft.AspNetCore.Identity;

namespace Med.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup("");

        endpoints.MapGroup("v1/medicines")
            .WithTags("Medicines")
            .RequireAuthorization()
            .MapEndpoint<CreateMedicineEndpoint>()
            .MapEndpoint<DeleteMedicineEndpoint>()
            .MapEndpoint<GetMedicineByIdEndpoint>()
            .MapEndpoint<GetMedicinesByUserEndpoint>()
            .MapEndpoint<MarkMedicineAsTakenEndpoint>()
            .MapEndpoint<SearchMedicineByNameEndpoint>();

        endpoints.MapGroup("v1/identity")
   .WithTags("Identity")
   .MapIdentityApi<IdentityUser<long>>();


        endpoints.MapGroup("v1/identity")
          .WithTags("Identity")
          .MapPost("/signout", async (
          SignInManager<IdentityUser<long>> signInManager,
          UserManager<IdentityUser<long>> s) =>
          {
              await signInManager.SignOutAsync();
              return Results.Ok();
          })
          .RequireAuthorization();

    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
    where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;

    }
}
