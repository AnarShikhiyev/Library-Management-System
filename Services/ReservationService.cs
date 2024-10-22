using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Models;
using Microsoft.EntityFrameworkCore;
using ProjectLibrary_Back.Interfaces;
namespace ProjectLibrary_Back.Services
{
    public class ReservationService : IReservationService
    {
        private readonly LibraryProjectContext _context;

        public ReservationService(LibraryProjectContext context)
        {
            _context = context;
        }
        public async Task<bool> AddReservationAsync(AddReservationDto dto)
        {
            var reservation = new Reservation
            {
                BookId = dto.BookId,
                UserId = dto.UserId,
                ReservationDate = dto.ReservationDate.HasValue
            ? DateOnly.FromDateTime(dto.ReservationDate.Value)
            : (DateOnly?)null,
                ExpirationDate = dto.ExpirationDate.HasValue
            ? DateOnly.FromDateTime(dto.ExpirationDate.Value)
            : (DateOnly?)null
            };
            _context.Reservations.Add(reservation);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CancelReservationAsync(int id)
        {
            var reservation = await _context.Reservations
                   .SingleOrDefaultAsync(r => r.Id ==id);

            if (reservation != null && reservation.Status != false)
            {
                reservation.Status= false;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> CheckAvailabilityAsync(int bookId, DateTime startDate, DateTime endDate)
        {
            var startDateOnly = DateOnly.FromDateTime(startDate);
            var endDateOnly = DateOnly.FromDateTime(endDate);

            var conflictingReservations = await _context.Reservations
                .Where(r => r.BookId == bookId &&
                            ((r.ReservationDate <= endDateOnly && r.ExpirationDate >= startDateOnly)))
                .AnyAsync();

            return !conflictingReservations;
        }

        public async Task<IEnumerable<ReservationDto>> GetBookReservationsAsync(int bookId)
        {
            return await _context.Reservations
                  .Where(r => r.BookId == bookId)
                  .Select(r => new ReservationDto
                  {
                      Id = r.Id,
                      ReservationDate = r.ReservationDate,
                      ExpirationDate = r.ExpirationDate,
                      BookId = r.BookId,
                      UserId = r.UserId
                  })
                  .ToListAsync();
        }

        public async Task<ReservationDto> GetReservationDetailsAsync(int id)
        {
            var reservation = await _context.Reservations
                 .Where(r => r.Id == id)
                 .Select(r => new ReservationDto
                 {
                     Id = r.Id,
                     ReservationDate = r.ReservationDate,
                     ExpirationDate = r.ExpirationDate,
                     BookId = r.BookId,
                     UserId = r.UserId
                 })
                 .FirstOrDefaultAsync();

            return reservation;
        }

        public async Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId)
        {
            return await _context.Reservations
                 .Where(r => r.UserId == userId)
                 .Select(r => new ReservationDto
                 {
                     Id = r.Id,
                     ReservationDate = r.ReservationDate,
                     ExpirationDate = r.ExpirationDate,
                     BookId = r.BookId,
                     UserId = r.UserId
                 })
                 .ToListAsync();
        }

        public async Task<bool> UpdateReservationAsync(int id, UpdateReservationDto dto)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return false;
            }

          
            
            
          
                reservation.ExpirationDate = DateOnly.FromDateTime(dto.ExpirationDate.Value);
           

            _context.Reservations.Update(reservation);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
