namespace ProjectLibrary_Back.DTO
{
    public class SendReminderNotificationDto
    {
        public int UserId { get; set; }
        public string Message { get; set; } = null!;
    }
}
