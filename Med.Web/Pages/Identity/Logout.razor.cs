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
        // Sem condicional: se o logout dependesse de uma checagem de auth que
        // faz request, uma falha de rede pularia o logout e a sessao ficaria viva.
        try
        {
            await Handler.LogoutAsync();
        }
        catch
        {
            // Mesmo se o servidor nao responder, seguimos para o reload abaixo.
        }

        await base.OnInitializedAsync();
    }

    #endregion

    #region Methods

    // Recarrega a aplicacao inteira em vez de chamar
    // NotifyAuthenticationStateChanged. O notify faria o AuthorizeRouteView
    // descartar e recriar esta pagina, reexecutando OnInitializedAsync; o
    // reload zera tudo e o estado de autenticacao e refeito do zero, a partir
    // do cookie que acabou de ser apagado.
    public void GoToLogin()
        => NavigationManager.NavigateTo("/login", forceLoad: true);

    #endregion
}
