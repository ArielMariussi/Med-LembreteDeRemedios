using Med.Api.Common;
using Med.Core.Handlers;
using Med.Core.Requests.Medicines;
using Med.Core.Responses;
using System.Security.Claims;

namespace Med.Api.Endpoints.Medicines
{
    public class DeleteMedicineEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapDelete("/{id:long}", HandleAsync)
            .WithName("Medicines: Delete")
            .WithSummary("Remove um medicamento")
            .WithOrder(6)
            .Produces<Response<bool>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IMedicineHandler handler,
            long id)
        {
            var request = new DeleteMedicineRequest
            {
                Id = id,
                UserId = user.Identity?.Name ?? string.Empty
            };

            var result = await handler.DeleteAsync(request);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.NotFound(result);
        }
    }
}
