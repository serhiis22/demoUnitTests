using static Subscription.Persistence.SD;
namespace Subscription.Persistence.Models;

public class ApiRequest
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public object Data { get; set; }
    //public string AccessToken { get; set; }
}