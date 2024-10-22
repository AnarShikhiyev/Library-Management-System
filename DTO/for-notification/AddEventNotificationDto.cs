namespace ProjectLibrary_Back.DTO
{
    public class AddEventNotificationDto
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string NotificationMessage { get; set; } = null!;
    }
}
