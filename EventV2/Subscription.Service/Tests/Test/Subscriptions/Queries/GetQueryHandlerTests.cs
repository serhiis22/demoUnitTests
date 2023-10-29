using AutoMapper;
using Shouldly;
using Subscription.Application.Subscriptions.Queries.GetSubscriptionList;
using Subscription.Persistence;
using Subscription.Test.Common;

namespace Subscription.Test.Subscriptions.Queries;

[Collection("QueryCollection")]
public class GetQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly SubscriptionRepository _repository;

    public GetQueryHandlerTests(QueryTestFixture fixture)
    {
        _repository = fixture.Repository;
        _mapper = fixture.Mapper;
    }
    

    [Fact]
    public async Task GetQueryHandler_Success()
    {
        // Arrange
        var handler = new GetSubscriptionListQueryHandler(_mapper, _repository);

        // Act
        var result = await handler.Handle(
            new GetSubscriptionListQuery
            {
                EventId = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084")
            },
            CancellationToken.None);

        // Assert
        result.ShouldBeOfType<SubscriptionListVm>();

    }
}