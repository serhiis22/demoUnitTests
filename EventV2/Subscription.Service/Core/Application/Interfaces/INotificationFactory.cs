using Subscription.Application.Models;

namespace Subscription.Application.Interfaces;

public interface INotificationFactory
{
     IList<Notification> CreateNotification(Domain.Subscription subscription, Event ev);
}