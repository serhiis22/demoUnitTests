using Moq;
using Subscription.Persistence;

namespace Subscription.Test.Common;

public abstract class TestCommandBase : IDisposable
{
    private readonly SubscriptionDbContext _context;
    protected readonly SubscriptionRepository Repository;
    protected readonly SubscriptionService Service;

    public TestCommandBase()
    {
        _context = SubscriptionsContextFactory.Create();
        Repository = new SubscriptionRepository(_context);

        // Mock setup for NotificationFactory
        var mockNotificationFactory = new Mock<NotificationFactory>();
        var factory1 = mockNotificationFactory.Object;

        // Mock setup for IHttpClientFactory
        var mockHttpClientFactory = new Mock<IHttpClientFactory>();
        var mockHttpClient = new Mock<HttpClient>();
        mockHttpClientFactory.Setup(factory => factory.CreateClient(It.IsAny<string>()))
            .Returns(mockHttpClient.Object);
        var httpClientFactory = mockHttpClientFactory.Object;
        Service = new SubscriptionService(factory1, httpClientFactory);// _context = SubscriptionsContextFactory.Create();
    }

    public void Dispose()
    {
        SubscriptionsContextFactory.Destroy(_context);
    }
}