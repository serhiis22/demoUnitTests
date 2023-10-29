using AutoMapper;
using Subscription.Application.Common.Mapping;
using Subscription.Persistence;
using Subscriptions.Application.Interfaces;

namespace Subscription.Test.Common;

public class QueryTestFixture : IDisposable
{
    private SubscriptionDbContext _context;
    public IMapper Mapper;
    public SubscriptionRepository Repository;

    public QueryTestFixture()
    {
        _context = SubscriptionsContextFactory.Create();
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile(
                typeof(ISubscriptionDbContext).Assembly));
        }); 
        Mapper = configurationProvider.CreateMapper();
        Repository = new SubscriptionRepository(_context);
    }

    public void Dispose()
    {
        SubscriptionsContextFactory.Destroy(_context);
    }
}

[CollectionDefinition("QueryCollection")]
public class QueryCollection : ICollectionFixture<QueryTestFixture> { }