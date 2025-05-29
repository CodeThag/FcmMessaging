namespace Utilities.Models
{
    public class NotificationLogModel
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string ResponseObject { get; set; } = string.Empty;
        public bool IsSuccessful { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
