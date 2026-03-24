using Med.Core.Handlers;
using Med.Core.Requests.Medicines;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Med.Core.Models;

namespace Med.Web.Pages;

public partial class HomePage : ComponentBase
{
    public List<Medicine> Medicines { get; set; } = [];
    [Inject] public IMedicineHandler Handler { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadMedicines();
    }

    protected async Task MarkAsTaken(long id)
    {
        await Handler.MarkAsTakenAsync(new MarkMedicineAsTakenRequest { Id = id });
        await LoadMedicines();
    }

    public bool IsPastDue(TimeOnly medTime)
    => medTime < TimeOnly.FromDateTime(DateTime.Now);

    public async Task LoadMedicines()
    {
        var result = await Handler.GetByUserAsync(new());
        if (result.IsSuccess)
            Medicines = result.Data.ToList();
        StateHasChanged();
    }
}