using ProjectLibrary_Back.DTO;

namespace ProjectLibrary_Back.Interfaces
{
    public interface IBookService

    {
        Task<int> AddBookAsync(AddBookDto newBook);
        Task<bool> UpdateBookAsync(int id, UpdateBookDto updatedBook);
        Task<bool> DeleteBookAsync(int bookId);
        Task<GetBookByIdDto> GetBookByIdAsync(int bookId);
        Task<IEnumerable<GetAllBooksDto>> GetAllBooksAsync();
        Task<IEnumerable<GetAllBooksDto>> SearchBooksAsync(string keyword);
        Task<IEnumerable<GetAllBooksDto>> FilterBooksAsync(FilterBooksDto filter);
        Task<GetBookInventoryDto> GetBookInventoryAsync(int bookId);
    }
}
