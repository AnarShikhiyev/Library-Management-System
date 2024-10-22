using Microsoft.EntityFrameworkCore;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Interfaces;
using ProjectLibrary_Back.Models;

namespace ProjectLibrary_Back.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryProjectContext _context;

        public AuthorService(LibraryProjectContext context)
        {
            _context = context;
        }
        public async Task<AuthorDto> AddAuthorAsync(CreateAuthorDto newAuthor)
        {
            var author = new Author
            {
                AuthorName = newAuthor.AuthorName,
                AuthorSurname = newAuthor.AuthorSurname,
                AuthorBirthDate = newAuthor.AuthorBirthDate,
                AuthorImg = newAuthor.AuthorImg
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return new AuthorDto
            {
                Id = author.Id,
                AuthorName = author.AuthorName,
                AuthorSurname = author.AuthorSurname,
                AuthorBirthDate = author.AuthorBirthDate,
                AuthorImg = author.AuthorImg
            };
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            return await _context.Authors
              .Select(a => new AuthorDto
              {
                  Id = a.Id,
                  AuthorName = a.AuthorName,
                  AuthorSurname = a.AuthorSurname,
                  AuthorBirthDate = a.AuthorBirthDate,
                  AuthorImg = a.AuthorImg
              })
              .ToListAsync();
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return null;

            return new AuthorDto
            {
                Id = author.Id,
                AuthorName = author.AuthorName,
                AuthorSurname = author.AuthorSurname,
                AuthorBirthDate = author.AuthorBirthDate,
                AuthorImg = author.AuthorImg
            };
        }

        public async Task<IEnumerable<AuthorDto>> GetAuthorByNameAsync(string name)
        {
            return await _context.Authors
             .Where(a => a.AuthorName.Contains(name) || a.AuthorSurname.Contains(name))
             .Select(a => new AuthorDto
             {
                 Id = a.Id,
                 AuthorName = a.AuthorName,
                 AuthorSurname = a.AuthorSurname,
                 AuthorBirthDate = a.AuthorBirthDate,
                 AuthorImg = a.AuthorImg
             })
             .ToListAsync();
        }

        public async Task<AuthorWithBooksDto?> GetBooksByAuthorAsync(int authorId)
        {
            var author = await _context.Authors
            .Include(a => a.Books)
            .Where(a => a.Id == authorId)
            .FirstOrDefaultAsync();

            if (author == null) return null;

            return new AuthorWithBooksDto
            {
                Id = author.Id,
                AuthorName = author.AuthorName,
                AuthorSurname = author.AuthorSurname,
                Books = author.Books.Select(b => new GetAthorBooksbyIdDto
                {
                    Id = b.Id,
                    BookTitle = b.BookTitle,
                    BookPrice = b.BookPrice,
                    BookImg = b.BookImg,
                    BookPage = b.BookPage,
                    BookPublicationYear = b.BookPublicationYear.HasValue ? b.BookPublicationYear.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null,
                    BookInventoryCount = b.BookInventoryCount
                }).ToList()
            };
        }

        public async Task<bool> RemoveBookFromAuthorAsync(int authorId, int bookId)
        {
            var book = await _context.Books
            .Where(b => b.Id == bookId && b.AuthorId == authorId)
            .FirstOrDefaultAsync();

            if (book == null) return false;

            book.AuthorId = null;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAuthorAsync(int id, UpdateAuthorDto updatedAuthor)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return false;

            author.AuthorName = updatedAuthor.AuthorName;
            author.AuthorSurname = updatedAuthor.AuthorSurname;
            author.AuthorBirthDate = updatedAuthor.AuthorBirthDate;
            author.AuthorImg = updatedAuthor.AuthorImg;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
