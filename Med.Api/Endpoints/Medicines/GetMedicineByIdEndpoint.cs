using Med.Api.Common;
using Med.Core.Handlers;
using Med.Core.Models;
using Med.Core.Requests.Medicines;
using Med.Core.Responses;
using System.Security.Claims;

namespace Med.Api.Endpoints.Medicines
{
    public class GetMedicineByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
      => app.MapGet("/{id:long}", HandleAsync)
        .WithName("Medicines: Get By Id")
        .WithSummary("Obtém um medicamento pelo ID")
        .WithOrder(2)
        .Produces<Response<Medicine?>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IMedicineHandler handler,
            long id)
        {
            var request = new GetMedicineByIdRequest
            {
                Id = id,
                UserId = user.Identity?.Name ?? string.Empty
            };

            var result = await handler.GetByIdAsync(request);

            return Results.Ok(result);
        }
    }
}
