namespace ProjectLibrary_Back.DTO
{
    public class GetUserRatingDTO
    {
        public string? UserName { get; set; }
        public string? BookTitle { get; set; }
        public byte? ratingCount { get; set; }
    }
}
