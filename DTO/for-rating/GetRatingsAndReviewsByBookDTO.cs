namespace ProjectLibrary_Back.DTO
{
    public class GetRatingsAndReviewsByBookDTO
    {
        public string? BookTitle { get; set; }
        public List<byte?> Ratings { get; set; } = new List<byte?>();
        public List<string?> Reviews { get; set; } = new List<string?>();
    }
}
