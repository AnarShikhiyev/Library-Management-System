namespace ProjectLibrary_Back.DTO
{
    public class RentalDto
    {
        public int RentalId { get; set; }
        public int? BookId { get; set; }
        public string? BookTitle { get; set; }
        public int? UserId { get; set; }
        public string? Username { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool Status { get; set; }
    }
}
