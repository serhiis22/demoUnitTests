using Subscription.Application.Models;
using Subscription.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscription.Test.Services
{
    public class NotificationFactoryTests
    {
        [Fact]
        public void CreateNotification_WithoutRecurrencePattern_CreatesSingleNotification()
        {
            // Arrange
            var guidId = Guid.NewGuid();
            var factory = new NotificationFactory();
            var subscription = new Domain.Subscription { Email = "test@email.com" };
            var ev = new Event { Id = guidId, Name = "Test Event", Date = DateTime.Today };

            // Act
            var notifications = factory.CreateNotification(subscription, ev);

            // Assert
            Assert.Single(notifications);
            Assert.Equal(ev.Id, notifications.First().EventId);
            Assert.Equal(ev.Name, notifications.First().Name);
            Assert.Equal(ev.Date, notifications.First().Date);
            Assert.Equal(subscription.Email, notifications.First().Email);
        }

        [Fact]
        public void CreateNotification_WithRecurrencePattern_CreatesMultipleNotifications()
        {
            // Arrange
            var guidId = Guid.NewGuid();
            var factory = new NotificationFactory();
            var subscription = new Domain.Subscription { Email = "test@email.com" };
            var ev = new Event
            {
                Id = guidId,
                Name = "Test Event",
                Date = DateTime.Today,
                RecurrencePattern = "FREQ=DAILY;UNTIL=20231231"
            };

            // Act
            var notifications = factory.CreateNotification(subscription, ev);

            // Assert
            Assert.NotEmpty(notifications);

        }
    }
}
