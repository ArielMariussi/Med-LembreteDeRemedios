using Med.Core.Handlers;
using Med.Core.Requests.Account;
using Med.Core.Responses;
using System.Net.Http.Json;
using System.Text;

namespace Med.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
        if (!result.IsSuccessStatusCode)
            return Response<string>.Fail("Usuario ou senha invalidos");

        return Response<string>.Ok("Login realizado com sucesso");
    }

    public async Task LogoutAsync()
    {
        var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
        await _client.PostAsync("v1/identity/signout", emptyContent);

    }

    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {

        var result = await _client.PostAsJsonAsync("v1/identity/register", request);
        if (!result.IsSuccessStatusCode)
            return Response<string>.Fail("Usuario ou senha invalidos");

        return Response<string>.Ok("Cadastro realizado com sucesso");
    }
}
