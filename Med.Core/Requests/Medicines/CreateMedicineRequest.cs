namespace Med.Core.Requests.Medicines;

public class CreateMedicineRequest : Request
{
    public string Name { get; set; } = string.Empty;

    public TimeSpan Time { get; set; }
}
