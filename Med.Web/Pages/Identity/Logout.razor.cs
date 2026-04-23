using Med.Core.Handlers;
using Med.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Med.Web.Pages.Identity;

public partial class LogoutPage : ComponentBase
{
    #region Services

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IAccountHandler Handler { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        //try
        //{
        //    await Handler.LogoutAsync();
        //    AuthenticationStateProvider.NotifyAuthenticationStateChanged();
        //}
        //catch { }

        //NavigationManager.NavigateTo("/login", forceLoad: true);
        //await base.OnInitializedAsync();

        Console.WriteLine("=== LOGOUT PAGE INICIANDO ===");
        try
        {
            await Handler.LogoutAsync();
            AuthenticationStateProvider.NotifyAuthenticationStateChanged();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"=== ERRO LOGOUT: {ex.Message} ===");
        }
        NavigationManager.NavigateTo("/login", forceLoad: true);
        await base.OnInitializedAsync();

    }

    #endregion
}
