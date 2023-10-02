namespace TodoWebService.Models.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Email { get; set; }
        public bool IsCompleted { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset UpdatedTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public bool isNotificationSended { get; set; }
        public string UserId { get; set; } = string.Empty;
        public virtual AppUser User { get; set; } = new();

    }
}