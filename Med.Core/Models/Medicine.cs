namespace Med.Core.Models;

public class Medicine
{
    public long Id { get; set; }

    public string Name{ get; set; } = string.Empty;

    public TimeOnly Time { get; set; }

    public string UserId { get; set; } = string.Empty;

    public bool Taken { get; set; }
}

