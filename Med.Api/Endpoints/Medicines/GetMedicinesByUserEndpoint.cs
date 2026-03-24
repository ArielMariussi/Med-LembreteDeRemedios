using Med.Api.Common;
using Med.Core.Handlers;
using Med.Core.Models;
using Med.Core.Requests.Medicines;
using Med.Core.Responses;
using System.Security.Claims;

namespace Med.Api.Endpoints.Medicines;

public class GetMedicinesByUserEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
 => app.MapGet("/user", HandleAsync)
            .WithName("Medicines: Get By User")
            .WithSummary("Lista medicamentos do usuário")
            .WithOrder(3)
            .Produces<Response<IEnumerable<Medicine>>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IMedicineHandler handler)
    {
        var request = new GetMedicinesByUserRequest
        {
            UserId = user.Identity?.Name ?? string.Empty
        };

        var result = await handler.GetByUserAsync(request);

        return Results.Ok(result);

    }
}
