namespace ProjectLibrary_Back.DTO
{
    public class AddRatingDTO
    {
        public byte? ratingCount { get; set; }
        public int? BookId { get; set; }
        public int? UserId { get; set; }
    }
}
