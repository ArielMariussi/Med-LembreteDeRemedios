namespace Med.Core.Requests.Medicines;

public class SearchMedicineByNameRequest : Request
{
    public string Name { get; set; } = string.Empty;
}
