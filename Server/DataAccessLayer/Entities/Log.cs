namespace DataAccessLayer.Entities;

public sealed class Log
{
    public int Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Logger { get; set; } = default!;
    public string Level { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string Action { get; set; } = default!;
    public string Method { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string Exception { get; set; } = default!;
}
