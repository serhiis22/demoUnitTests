namespace Subscription.Application.Models;

public class Notification
{
    public Guid EventId { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Images { get; set; }
    public string? Description { get; set; }
    public string? Place { get; set; }
    public DateTime Date { get; set; }
    public string? AdditionalInfo { get; set; }
    public string Email { get; set; }
}