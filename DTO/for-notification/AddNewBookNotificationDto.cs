namespace ProjectLibrary_Back.DTO
{
    public class AddNewBookNotificationDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string NotificationMessage { get; set; } = null!;
    }
}
