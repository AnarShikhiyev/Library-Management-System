namespace ProjectLibrary_Back.DTO
{
    public class ReminderDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public DateTime? ReminderDate { get; set; }
        public string ReminderMessage { get; set; }
    }
}
