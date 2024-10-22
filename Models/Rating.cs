namespace ProjectLibrary_Back.Models
{
    public partial class Rating
    {
        public int Id { get; set; }
        public byte? ratingCount { get; set; }
        public int? BookId { get; set; }
        public int? UserId { get; set; }
        public virtual Book? Book { get; set; }
        public virtual User? User { get; set; }

    }
}
