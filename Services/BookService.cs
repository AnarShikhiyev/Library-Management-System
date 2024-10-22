using Microsoft.EntityFrameworkCore;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Interfaces;
using ProjectLibrary_Back.Models;

namespace ProjectLibrary_Back.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryProjectContext _context;

        public BookService(LibraryProjectContext context)
        {
            _context = context;
        }


        public async Task<int> AddBookAsync(AddBookDto newBook)
        {
            var book = new Book
            {
                BookTitle = newBook.BookTitle,
                BookPrice = newBook.BookPrice,
                BookImg = newBook.BookImg,
                BookPage = newBook.BookPage,
                BookPublicationYear = DateOnly.FromDateTime(newBook.BookPublicationYear),
                BookInventoryCount = newBook.BookInventoryCount,
                AuthorId = newBook.AuthorId,
                CategoryId = newBook.CategoryId,
                LanguageId = newBook.LanguageId
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<GetAllBooksDto>> FilterBooksAsync(FilterBooksDto filter)
        {

            var query = _context.Books.AsQueryable();

            
            if (filter.AuthorId.HasValue)
                query = query.Where(b => b.AuthorId == filter.AuthorId.Value);
            if (filter.CategoryId.HasValue)
                query = query.Where(b => b.CategoryId == filter.CategoryId.Value);
            if (filter.LanguageId.HasValue)
                query = query.Where(b => b.LanguageId == filter.LanguageId.Value);
           

        
            return await query
                .Select(b => new GetAllBooksDto
                {
                    Id = b.Id,
                    BookTitle = b.BookTitle,
                    BookPrice = b.BookPrice,
                    BookImg = b.BookImg,
                    BookPage = b.BookPage,
                    BookPublicationYear = b.BookPublicationYear.HasValue
                        ? b.BookPublicationYear.Value.ToDateTime(TimeOnly.MinValue)
                        : (DateTime?)null,
                    BookInventoryCount = b.BookInventoryCount,
                    AuthorId = b.AuthorId,
                    CategoryId = b.CategoryId,
                    LanguageId = b.LanguageId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<GetAllBooksDto>> GetAllBooksAsync()
        {
            return await _context.Books
          .Select(b => new GetAllBooksDto
          {
              Id = b.Id,
              BookTitle = b.BookTitle,
              BookPrice = b.BookPrice,
              BookImg = b.BookImg,
              BookPage = b.BookPage,
              BookPublicationYear = b.BookPublicationYear.HasValue
                ? b.BookPublicationYear.Value.ToDateTime(TimeOnly.MinValue)
                : (DateTime?)null,
              BookInventoryCount = b.BookInventoryCount,
              AuthorId = b.AuthorId,
              CategoryId = b.CategoryId,
              LanguageId = b.LanguageId
          })
          .ToListAsync();
        }

        public async Task<GetBookByIdDto> GetBookByIdAsync(int bookId)
        {
            var book = await _context.Books
             .Where(b => b.Id == bookId)
             .Select(b => new GetBookByIdDto
             {
                 Id = b.Id,
                 BookTitle = b.BookTitle,
                 BookPrice = b.BookPrice,
                 BookImg = b.BookImg,
                 BookPage = b.BookPage,
                 BookPublicationYear = b.BookPublicationYear.HasValue
                ? b.BookPublicationYear.Value.ToDateTime(TimeOnly.MinValue)
                : (DateTime?)null,
                 BookInventoryCount = b.BookInventoryCount,
                 AuthorId = b.AuthorId,
                 CategoryId = b.CategoryId,
                 LanguageId = b.LanguageId
             })
              .FirstOrDefaultAsync();

            return book;
        }

        public async Task<GetBookInventoryDto> GetBookInventoryAsync(int bookId)
        {
            return await _context.Books
             .Where(b => b.Id == bookId)
             .Select(b => new GetBookInventoryDto
             {
                 Id = b.Id,
                 BookTitle = b.BookTitle,
                 BookInventoryCount = b.BookInventoryCount
             })
             .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GetAllBooksDto>> SearchBooksAsync(string keyword)
        {
            return await _context.Books
            .Where(b => b.BookTitle.Contains(keyword))
            .Select(b => new GetAllBooksDto
            {
                Id = b.Id,
                BookTitle = b.BookTitle,
                BookPrice = b.BookPrice,
                BookImg = b.BookImg,
                BookPage = b.BookPage,
                BookPublicationYear = b.BookPublicationYear.HasValue ? b.BookPublicationYear.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                BookInventoryCount = b.BookInventoryCount,
                AuthorId = b.AuthorId,
                CategoryId = b.CategoryId,
                LanguageId = b.LanguageId
            })
            .ToListAsync();
        }

        public async Task<bool> UpdateBookAsync(int id,UpdateBookDto updatedBook)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

          
            book.BookTitle = updatedBook.BookTitle;
            book.BookPrice = updatedBook.BookPrice;
            book.BookImg = updatedBook.BookImg;
            book.BookPage = updatedBook.BookPage;
            book.BookPublicationYear = updatedBook.BookPublicationYear.HasValue
       ? DateOnly.FromDateTime(updatedBook.BookPublicationYear.Value)
       : (DateOnly?)null;
            book.BookInventoryCount = updatedBook.BookInventoryCount;
            

            await _context.SaveChangesAsync();

            return true; 
        }
    }
}
