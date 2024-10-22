namespace ProjectLibrary_Back.DTO
{
    public class AddReviewDTO
    {
        public string? ReviewText { get; set; }
        public int? BookId { get; set; }
        public int? UserId { get; set; }
    }
}
