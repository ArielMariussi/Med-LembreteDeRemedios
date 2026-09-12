using Med.Core.Handlers;
using Med.Core.Requests.Medicines;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Med.Core.Models;
using MudBlazor;

namespace Med.Web.Layout;

public partial class MainLayout : LayoutComponentBase, IAsyncDisposable
{
    [Inject] public IMedicineHandler Handler { get; set; } = null!;
    [Inject] public IJSRuntime JS { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    private bool _isDrawerOpened = true;
    private Timer? _notificationTimer;
    private List<Medicine> _medicines = [];
    private readonly HashSet<long> _notified = [];
    private string _notificationPermission = "default";

    private bool ShowEnableNotificationsButton => _notificationPermission != "granted";

    private void ToggleDrawer() => _isDrawerOpened = !_isDrawerOpened;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JS.InvokeVoidAsync("requestNotificationPermission");
            _notificationPermission = await JS.InvokeAsync<string>("getNotificationPermission");
            StateHasChanged();
            await LoadMedicines();
            StartNotificationTimer();
        }
    }

    private async Task EnableNotifications()
    {
        if (_notificationPermission == "denied")
        {
            Snackbar.Add(
                "As notificações foram bloqueadas para este site. Habilite manualmente nas configurações do navegador (ícone de cadeado na barra de endereço).",
                Severity.Warning);
            return;
        }

        await JS.InvokeVoidAsync("requestNotificationPermission");
        _notificationPermission = await JS.InvokeAsync<string>("getNotificationPermission");

        if (_notificationPermission == "granted")
            Snackbar.Add("Notificações ativadas!", Severity.Success);
    }

    private void StartNotificationTimer()
    {
        _notificationTimer?.Dispose();
        _notificationTimer = new Timer(async _ =>
        {
            await CheckAndNotify();
        }, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
    }

    private async Task CheckAndNotify()
    {
        var now = TimeOnly.FromDateTime(DateTime.Now);
        foreach (var med in _medicines)
        {
            if (med.Taken || _notified.Contains(med.Id))
                continue;

            var diff = (now.ToTimeSpan() - med.Time.ToTimeSpan()).TotalMinutes;
            if (diff is >= 0 and <= 1)
            {
                _notified.Add(med.Id);
                await InvokeAsync(async () =>
                {
                    await JS.InvokeVoidAsync(
                        "showMedicineNotification",
                        med.Name,
                        med.Time.ToString(@"HH\:mm")
                    );
                });
            }
        }
    }

    private async Task LoadMedicines()
    {
        var result = await Handler.GetByUserAsync(new());
        if (result.IsSuccess && result.Data != null)
            _medicines = result.Data.ToList();
    }

    public async ValueTask DisposeAsync()
    {
        if (_notificationTimer is not null)
            await _notificationTimer.DisposeAsync();
    }
}