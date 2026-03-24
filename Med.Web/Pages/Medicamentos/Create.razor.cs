using Med.Core.Handlers;
using Med.Core.Models;
using Med.Core.Requests.Medicines;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Reflection;
using System.Security.Claims;

namespace Med.Web.Pages.Medicamentos;

public partial class CreatePage : ComponentBase
{
    [Inject] public IMedicineHandler Handler { get; set; } = null!;
    [Inject] public NavigationManager Nav { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    public TimeSpan? Time { get; set; } = DateTime.Now.TimeOfDay;

    public string Name { get; set; } = string.Empty;


    public async Task HandleCreate()
    {
        Console.WriteLine("CLICOU");

        try
        {
            var medicine = new CreateMedicineRequest
            {
                Name = Name,
                Time = Time ?? DateTime.Now.TimeOfDay
            };

            var result = await Handler.CreateAsync(medicine);

            if (result.IsSuccess)
            {
                Console.WriteLine("SUCESSO");
                Nav.NavigateTo("/medicines", true);
            }
            else
            {
                Console.WriteLine($"ERRO: {result.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"EXCEPTION: {ex.Message}");
        }
 
    }
}

