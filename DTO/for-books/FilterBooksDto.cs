namespace ProjectLibrary_Back.DTO
{
    public class FilterBooksDto
    {
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
        public int? LanguageId { get; set; }
        public DateTime? PublicationYear { get; set; }
    }
}
