using Med.Core.Handlers;
using Med.Core.Models;
using Med.Core.Requests.Medicines;
using Microsoft.AspNetCore.Components;

namespace Med.Web.Pages.Medicamentos
{
    public partial class SearchPage :ComponentBase
    {
        public string SearchTerm { get; set; } = string.Empty;
        public List<Medicine> Medicines { get; set; } = [];

        [Inject] public NavigationManager Nav { get; set; } = null!;
        [Inject] public IMedicineHandler Handler { get; set; } = null!;

        public  bool _searched = false;

        public async Task HandleSearch()
        {
            Medicines = [];
            var result = await Handler.SearchByNameAsync(
                new SearchMedicineByNameRequest { Name = SearchTerm });

            if (result.IsSuccess)
                Medicines = result.Data.ToList();

            _searched = true;
            StateHasChanged();
        }

        public void GoToMedicine(long id)
        {
            Nav.NavigateTo($"/medicines?id={id}");
        }


    }
}
