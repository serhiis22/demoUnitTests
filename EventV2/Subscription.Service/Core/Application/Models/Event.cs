namespace Subscription.Application.Models;

public class Event
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Images { get; set; }
    public string Description { get; set; }
    public string Place { get; set; }
    public DateTime Date { get; set; }
    public string AdditionalInfo { get; set; }
    public string? RecurrencePattern { get; set; }
}