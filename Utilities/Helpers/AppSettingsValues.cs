namespace Utilities.Helpers;

public class AppSettingsValues
{
    public string CronSchedule { get; set; } = string.Empty;
    public string CustomBaseUrl { get; set; } = string.Empty;
    public string SubscriptionKey { get; set; } = string.Empty;
    public string AppId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string MessageContent { get; set; } = string.Empty;
}


public class ConnectionStringValues
{
    public string DefaultConnection { get; set; } = string.Empty;
    public string FirstDefaultConnection { get; set; } = string.Empty;
}
