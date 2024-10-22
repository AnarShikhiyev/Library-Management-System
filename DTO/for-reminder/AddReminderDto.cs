namespace ProjectLibrary_Back.DTO
{
    public class AddReminderDto
    {
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public DateTime? ReminderDate { get; set; }
        public string ReminderMessage { get; set; } = null!;
    }
}
