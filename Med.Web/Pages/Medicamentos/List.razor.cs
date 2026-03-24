using Med.Core.Handlers;
using Med.Core.Models;
using Med.Core.Requests.Medicines;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Med.Web.Pages.Medicamentos;

public partial class ListPage: ComponentBase
{
    [Parameter]
    [SupplyParameterFromQuery]
    public long? Id { get; set; }
    public List<Medicine> Medicines { get; set; } = [];
    public Medicine? SelectedMedicine { get; set; }

    [Inject] public AuthenticationStateProvider AuthState { get; set; } = null!;
    [Inject] public NavigationManager Nav { get; set; } = null!;

     [Inject] public IMedicineHandler Handler { get; set; } = null!;

    protected override async Task OnParametersSetAsync()
    {
        await LoadMedicines();

        if (Id.HasValue)
        {
            SelectedMedicine = Medicines.FirstOrDefault(x => x.Id == Id.Value);
        }
    }

    public async Task LoadMedicines()
    {
        var result = await Handler.GetByUserAsync(new GetMedicinesByUserRequest());

        if (result.IsSuccess && result.Data != null)
            Medicines = result.Data.ToList();
    }

    public async Task Delete(long id)
    {
        await Handler.DeleteAsync(new DeleteMedicineRequest { Id = id });

        Medicines.RemoveAll(x => x.Id == id);

        if (SelectedMedicine?.Id == id)
            SelectedMedicine = null;
    }

    public Task GoToMedicine(long id)
    {
        Nav.NavigateTo($"/medicines?id={id}");
        return Task.CompletedTask;
    }

    public string RowStyle(Medicine item, int index)
    {
        var style = "cursor:pointer;";
        if (item.Id == Id) 
            style += "background-color:#d3f9d8;";

        return style;
    }
    public async Task ToggleTaken(Medicine med)
    {
        if (med.Taken) return;

        var result = await Handler.MarkAsTakenAsync(new MarkMedicineAsTakenRequest
        {
            Id = med.Id
        });

        if (result.IsSuccess)
        {
            med.Taken = true;
            StateHasChanged();
        }
    }

}
