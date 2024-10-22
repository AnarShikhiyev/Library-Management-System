namespace ProjectLibrary_Back.DTO
{
    public class GetMostReadBooksDTO
    {
        public int BookId { get; set; }
        public string? BookTitle { get; set; }
        public int ReadCount { get; set; }

    }
}
