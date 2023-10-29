using Subscription.Persistence.Models;

namespace Subscription.Persistence.Interfaces;

public interface IBaseService: IDisposable
{
    ResponseDto ResponseModel { get; set; }
    Task<T> SendAsync<T>(ApiRequest apiRequest);
}