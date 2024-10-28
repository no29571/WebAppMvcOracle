namespace WebAppMvc.Models
{
    //https://learn.microsoft.com/ja-jp/ef/core/logging-events-diagnostics/events
    public interface IHasTimestamps
    {
        DateTime? CreatedAt { get; set; }
        DateTime? UpdatedAt { get; set; }
    }
}
