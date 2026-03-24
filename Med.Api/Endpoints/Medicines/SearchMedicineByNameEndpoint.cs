using Med.Api.Common;
using Med.Core.Handlers;
using Med.Core.Models;
using Med.Core.Requests.Medicines;
using Med.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Med.Api.Endpoints.Medicines;

public class SearchMedicineByNameEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/search", HandleAsync)
            .WithName("Medicines: Search By Name")
            .WithSummary("Busca medicamentos pelo nome")
            .WithOrder(4)
            .Produces<Response<IEnumerable<Medicine>>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IMedicineHandler handler,
        [FromQuery] string name)
    {
        var request = new SearchMedicineByNameRequest
        {
            Name = name,
            UserId = user.Identity?.Name ?? string.Empty
        };

        var result = await handler.SearchByNameAsync(request);

        return Results.Ok(result);
    }
}
