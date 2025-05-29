namespace Utilities.Models
{
    public class NotificationsResponseModel
    {
        public DateTime Timestamp { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string Data { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
