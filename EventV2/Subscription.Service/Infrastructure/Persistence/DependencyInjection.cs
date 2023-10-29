using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Subscription.Application.Interfaces;
using Subscriptions.Application.Interfaces;

namespace Subscription.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection
        services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");
        services.AddDbContext<SubscriptionDbContext>(options => { options.UseSqlServer(connectionString); });
        services.AddScoped<ISubscriptionDbContext>(provider =>
            provider.GetService<SubscriptionDbContext>());

        SD.EventAPIBase = configuration["ServiceUrls:EventAPI"];
        services.AddHttpClient<ISubscriptionService, SubscriptionService>("EventApi", c =>
        {
            if (SD.EventAPIBase != null) c.BaseAddress = new Uri(SD.EventAPIBase);
        });
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<INotificationFactory, NotificationFactory>();
        return services;
    }
}