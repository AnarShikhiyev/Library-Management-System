namespace ProjectLibrary_Back.DTO
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string NotificationMessage { get; set; }
        public DateTime? NotificationDate { get; set; }
        public int? BookId { get; set; }
        public int? EventId { get; set; }
    }
}
