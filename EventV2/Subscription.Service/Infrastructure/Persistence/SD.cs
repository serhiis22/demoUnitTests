namespace Subscription.Persistence;

public static class SD
{
    public static string EventAPIBase { get; set; }
    public static string NotificationAPIBase { get; set; }
    public enum ApiType
    {
        GET,
        POST,
        PUT,
        DELETE
    }
}