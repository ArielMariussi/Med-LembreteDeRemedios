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
        // NotifyAuthenticationStateChanged faz o AuthorizeRouteView descartar e
        // recriar esta pagina, ou seja, OnInitializedAsync roda de novo. So nao
        // vira loop porque na segunda passada o cookie ja foi apagado e
        // CheckAuthenticatedAsync retorna false.
        if (await AuthenticationStateProvider.CheckAuthenticatedAsync())
        {
            await Handler.LogoutAsync();
            AuthenticationStateProvider.NotifyAuthenticationStateChanged();
        }

        await base.OnInitializedAsync();
    }

    #endregion
}
