using Serilog;
using Subscription.Application.Interfaces;
using Subscription.Application.Models;
using Subscription.Persistence.Models;

namespace Subscription.Persistence;

public class SubscriptionService : BaseService, ISubscriptionService
{
    private readonly INotificationFactory _notificationFactory;
    private readonly HttpClient _httpClient;

    public SubscriptionService(INotificationFactory notificationFactory, IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
        _notificationFactory = notificationFactory;
        _httpClient = httpClientFactory.CreateClient("EventApi");
    }

    public async Task CreateNotifications(Domain.Subscription subscription)
    {
        var ev = await GetEventById<Event>(subscription.EventId);
        var notifications = _notificationFactory.CreateNotification(subscription, ev);
        await SendNotifications(notifications);
    }

    public async Task RemoveNotifications(Guid eventId)
    {
        await RemoveNotification<ResponseDto>(eventId);
    }

    private async Task SendNotifications(IList<Notification> notifications)
    {
        foreach (var notification in notifications)
        {
            var send = await SendNotification<ResponseDto>(notification);
            if (send == null && !send.IsSuccess)
            {
                Log.Warning($"Not send notification{send.DisplayMessage}");
            }
        }
    }

    private async Task<T> SendNotification<T>(Notification notification)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Url = SD.EventAPIBase + "/api/notification",
            Data = notification
            //AccessToken = token
        });
    }

    public async Task<T> RemoveNotification<T>(Guid eventId)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Url = SD.EventAPIBase + "/api/notifications",
            Data = eventId
            //AccessToken = token
        });
    }


    private async Task<T> GetEventById<T>(Guid id)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.EventAPIBase + "/api/event/"+id,
            //AccessToken = token
        });
    }
}