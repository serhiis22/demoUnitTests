using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Subscription.Application.Interfaces;
using Subscription.Application.Models;

namespace Subscription.Persistence;

public class NotificationFactory : INotificationFactory
{
    public IList<Notification> CreateNotification(Domain.Subscription subscription, Event ev)
    {
        var notifications = new List<Notification>();
        var dates = new List<DateTime>(); 
        
        if (!string.IsNullOrEmpty(ev.RecurrencePattern))
        {
            dates = GetDatesFromRRule(ev);
        }
        else
        {
            dates.Add(ev.Date);
        }
        
        foreach (var date in dates)
        {
            var notification = new Notification
            {
                EventId = ev.Id,
                Name = ev.Name,
                Category = ev.Category,
                Images = ev.Images,
                Description = ev.Description,
                Place = ev.Place,
                Date = ev.Date,
                AdditionalInfo = ev.AdditionalInfo,
                Email = subscription.Email
            };
            notifications.Add(notification);
        }
      
        

        return notifications;
    }

    private static List<DateTime> GetDatesFromRRule(Event ev)
    {
        string rruleStr = ev.RecurrencePattern;
        var rrule = new RecurrencePattern(rruleStr);

        if (rrule.Until == DateTime.MinValue)
        {
            throw new ArgumentException("RRULE must have an UNTIL date", nameof(rruleStr));
        }

        var evt = new CalendarEvent
        {
            Start = new CalDateTime(ev.Date),
            RecurrenceRules = new List<RecurrencePattern> { rrule }
        };

        var occurrences = evt.GetOccurrences(DateTime.UtcNow, rrule.Until).Select(o => o.Period.StartTime.AsSystemLocal).ToList();

        return occurrences;
    }
}