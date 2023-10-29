using Moq.Protected;
using Moq;
using Subscription.Persistence.Models;
using Subscription.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Subscription.Test.Services
{
    public class BaseServiceTests
    {
        [Fact]
        public async Task SendAsync_GetRequest_ReturnsExpectedResult()
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var httpClient = new HttpClient();
            var apiResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{ \"key\": \"value\" }")
            };
            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(apiResponse);

            httpClient = new HttpClient(handlerMock.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var service = new BaseService(httpClientFactoryMock.Object);
            var request = new ApiRequest { Url = "https://api.url", ApiType = SD.ApiType.GET };

            // Act
            var result = await service.SendAsync<Dictionary<string, string>>(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("value", result["key"]);
        }
    }
}
