namespace ProjectLibrary_Back.DTO
{
    public class AuthorWithBooksDto
    {
        public int Id { get; set; }
        public string AuthorName { get; set; } = null!;
        public string AuthorSurname { get; set; } = null!;

        public List<GetAthorBooksbyIdDto> Books { get; set; } = new List<GetAthorBooksbyIdDto>();
    }
}
