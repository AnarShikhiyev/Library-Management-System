using Microsoft.EntityFrameworkCore;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Interfaces;
using ProjectLibrary_Back.Models;

namespace ProjectLibrary_Back.Services
{
    public class BookRentalService : IBookRentalService
    {
        private readonly LibraryProjectContext _context;

        public BookRentalService(LibraryProjectContext context)
        {
            _context = context;
        }
        public async Task<List<Rental>> CheckOverdueBooksAsync(DateTime currentDate)
        {
            return await _context.Rentals
                      .Where(r => r.DueDate < DateOnly.FromDateTime(currentDate) && r.status)
                      .ToListAsync();
        }

        public async Task<bool> LogReturnEventAsync(int bookId, int userId, DateTime returnDate)
        {
            var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.BookId == bookId && r.UserId == userId && r.status == true);
            if (rental != null)
            {
                rental.status = false;
                rental.DueDate = DateOnly.FromDateTime(returnDate);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> SendOverdueNoticesAsync(List<Rental> overdueBooks)
        {
            foreach (var rental in overdueBooks)
            {
                if (rental.BookId.HasValue && rental.status == true)
                {
                    var message = $"Hörmətli {rental.User?.Username}, zəhmət olmasa '{rental.Book?.BookTitle}' adlı kitabı geri qaytarın.";

                  
                    Console.WriteLine(message);
                }
            }

            return true;
        }

        public async Task<bool> UpdateBookStatusAsync(int rentalId)
        {
            var rental = await _context.Rentals.FindAsync(rentalId);
            if (rental != null)
            {
                rental.status = false;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> AddRentalAsync(AddRentalDto rentalDto)
        {
            var rental = new Rental
            {
                BookId = rentalDto.BookId,
                UserId = rentalDto.UserId,
                RentalDate = DateOnly.FromDateTime(rentalDto.RentalDate),
                DueDate = DateOnly.FromDateTime(rentalDto.DueDate),
                status = true 
            };

            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return true;
        }

     
        public async Task<bool> UpdateRentalAsync(UpdateRentalDto updateDto)
        {
            var rental = await _context.Rentals.FindAsync(updateDto.RentalId);
            if (rental != null)
            {
                rental.DueDate = DateOnly.FromDateTime(updateDto.NewDueDate);
                rental.status = !updateDto.IsReturned; 

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<RentalDto> GetRentalByIdAsync(int rentalId)
        {
            var rental = await _context.Rentals
                .Include(r => r.User)
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.Id == rentalId);

            if (rental == null)
            {
                return null;
            }

            return new RentalDto
            {
                RentalId = rental.Id,
                BookId = rental.BookId,
                BookTitle = rental.Book?.BookTitle,
                UserId = rental.UserId,
                Username = rental.User?.Username,
                RentalDate = rental.RentalDate.ToDateTime(TimeOnly.MinValue),
                DueDate = rental.DueDate.ToDateTime(TimeOnly.MinValue),
                Status = rental.status
            };
        }
    }
}
