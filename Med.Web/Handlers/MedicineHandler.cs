using Med.Core.Handlers;
using Med.Core.Models;
using Med.Core.Requests.Medicines;
using Med.Core.Responses;
using System.Net.Http.Json;

namespace Med.Web.Handlers;

public class MedicineHandler(IHttpClientFactory httpClientFactory) : IMedicineHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<Medicine>> CreateAsync(CreateMedicineRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/medicines", request);

        if (!result.IsSuccessStatusCode)
            return Response<Medicine>.Fail("Nao foi possivel cadastrar o medicamento");

        return await result.Content.ReadFromJsonAsync<Response<Medicine>>()
            ?? Response<Medicine>.Fail("Erro inesperado");

    }

    public async  Task<Response<bool>> DeleteAsync(DeleteMedicineRequest request)
    {
        var result = await _client.DeleteAsync($"v1/medicines/{request.Id}");

        if (!result.IsSuccessStatusCode)
            return Response<bool>.Fail("Erro ao deletar medicamento");

        return await result.Content.ReadFromJsonAsync<Response<bool>>()
            ?? Response<bool>.Fail("Erro inesperado");

    }

    public async Task<Response<Medicine?>> GetByIdAsync(GetMedicineByIdRequest request)
    {
        return await _client.GetFromJsonAsync<Response<Medicine?>>($"v1/medicines/{request.Id}")
             ?? Response<Medicine?>.Fail("Medicamento nao encontrado");
    }

    public async  Task<Response<IEnumerable<Medicine>>> GetByUserAsync(GetMedicinesByUserRequest request)
    {
        return await _client.GetFromJsonAsync<Response<IEnumerable<Medicine>>>($"v1/medicines/user")
             ?? Response<IEnumerable<Medicine>>.Fail("Erro ao buscar medicamentos");
    }

    public async  Task<Response<Medicine>> MarkAsTakenAsync(MarkMedicineAsTakenRequest request)
    {
        var result = await _client.PostAsJsonAsync($"v1/medicines/taken/{request.Id}", request);

        if (!result.IsSuccessStatusCode)
            return Response<Medicine>.Fail("Erro ao marcar medicamento como tomado");

        return await result.Content.ReadFromJsonAsync<Response<Medicine>>()
            ?? Response<Medicine>.Fail("Erro inesperado");
    }

    public async Task<Response<IEnumerable<Medicine>>> SearchByNameAsync(SearchMedicineByNameRequest request)
    {
        return await _client.GetFromJsonAsync<Response<IEnumerable<Medicine>>>($"v1/medicines/search?name={request.Name}")
            ?? Response<IEnumerable<Medicine>>.Fail("Erro na busca");
    }
}
