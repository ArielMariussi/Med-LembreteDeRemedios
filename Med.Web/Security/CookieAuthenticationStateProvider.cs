using Med.Core.Models.Account;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Security.Principal;

namespace Med.Web.Security
{
    public class CookieAuthenticationStateProvider(IHttpClientFactory clientFactory) :
        AuthenticationStateProvider, ICookieAuthenticationStateProvider
    {
        private bool _isAuthenticated = false;
        private readonly HttpClient _client = clientFactory.CreateClient(Configuration.HttpClientName);
        public async Task<bool> CheckAuthenticatedAsync()
        {
            await GetAuthenticationStateAsync();
            return _isAuthenticated;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _isAuthenticated = false;

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            var userInfo = await GetUser();
            if (userInfo is null)
                return new AuthenticationState(user);

            identity = new ClaimsIdentity(
       [
           new Claim(ClaimTypes.Name, userInfo.Email),
            new Claim(ClaimTypes.Email, userInfo.Email)
       ], nameof(CookieAuthenticationStateProvider));

            user = new ClaimsPrincipal(identity);

            _isAuthenticated = true;
            return new AuthenticationState(user);
        }

        public void NotifyAuthenticationStateChanged()
            => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        private async Task<User?> GetUser()
        {
            try
            {
                return await _client.GetFromJsonAsync<User?>("v1/identity/manage/info");

            }
            catch
            {
                return null;
            }
        }
        
    } 
}
