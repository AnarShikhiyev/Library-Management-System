using ProjectLibrary_Back.DTO;

namespace ProjectLibrary_Back.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDto> AddAuthorAsync(CreateAuthorDto newAuthor);
        Task<bool> UpdateAuthorAsync(int id, UpdateAuthorDto updatedAuthor);
        Task<bool> DeleteAuthorAsync(int id);
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();
        Task<IEnumerable<AuthorDto>> GetAuthorByNameAsync(string name);
        Task<bool> RemoveBookFromAuthorAsync(int authorId, int bookId);
        Task<AuthorWithBooksDto?> GetBooksByAuthorAsync(int authorId);
    }
}
