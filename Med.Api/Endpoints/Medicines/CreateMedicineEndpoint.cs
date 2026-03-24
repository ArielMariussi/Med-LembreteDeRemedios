using Med.Api.Common;
using Med.Core.Handlers;
using Med.Core.Models;
using Med.Core.Requests.Medicines;
using Med.Core.Responses;
using System.Security.Claims;

namespace Med.Api.Endpoints.Medicines
{
    public class CreateMedicineEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Medicines: Create")
            .WithSummary("Cria um novo medicamento")
            .WithDescription("Cria um novo lembrete de medicamento")
            .WithOrder(1)
            .Produces<Response<Medicine>>();

        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            IMedicineHandler handler,
            CreateMedicineRequest request)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;

            var result = await handler.CreateAsync(request);

            return result.IsSuccess
                ? Results.Ok(result)
                : Results.BadRequest(result);

        }
    }
}
