namespace ProjectLibrary_Back.DTO
{
    public class GetUserReviewDTO
    {
        public string? UserName { get; set; }
        public string? BookTitle { get; set; }
        public string? ReviewText { get; set; }
        public DateTime? ReviewDate { get; set; }
    }
}
