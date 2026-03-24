using Med.Api.Common;
using Med.Core.Handlers;
using Med.Core.Models;
using Med.Core.Requests.Medicines;
using Med.Core.Responses;
using System.Security.Claims;

namespace Med.Api.Endpoints.Medicines;

public class MarkMedicineAsTakenEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
  => app.MapPost("/taken/{id:long}", HandleAsync)
      .WithName("Medicines: Mark As Taken")
      .WithSummary("Marca medicamento como tomado")
      .WithOrder(5)
      .Produces<Response<Medicine>>();

    private static async Task<IResult> HandleAsync(
        ClaimsPrincipal user,
        IMedicineHandler handler,
        long id)
    {
        var request = new MarkMedicineAsTakenRequest
        {
            Id = id,
            UserId = user.Identity?.Name ?? string.Empty
        };

        var result = await handler.MarkAsTakenAsync(request);

        return result.IsSuccess
            ? Results.Ok(result)
            : Results.BadRequest(result);
    }
}