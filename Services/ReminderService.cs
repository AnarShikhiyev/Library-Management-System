using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Models;
using Microsoft.EntityFrameworkCore;
using ProjectLibrary_Back.Interfaces;

namespace ProjectLibrary_Back.Services
{
    public class ReminderService : IReminderService
    {
        private readonly LibraryProjectContext _context;

        public ReminderService(LibraryProjectContext context)
        {
            _context = context;
        }
        public async Task<bool> AddReminderAsync(AddReminderDto dto)
        {
            var reminder = new Remender
            {
                BookId = dto.BookId,
                UserId = dto.UserId,
                ReminderDate = dto.ReminderDate.HasValue ? DateOnly.FromDateTime(dto.ReminderDate.Value) : default,
                ReminderMessage = dto.ReminderMessage
            };
            _context.Remenders.Add(reminder);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ReminderDto>> GetRemindersByUserAsync(int userId)
        {
            return await _context.Remenders
            .Where(r => r.UserId == userId)
            .Select(r => new ReminderDto
            {
                Id = r.Id,
                BookId = r.BookId,
                UserId = r.UserId,
                ReminderDate = r.ReminderDate.ToDateTime(TimeOnly.MinValue),
                ReminderMessage = r.ReminderMessage
            })
            .ToListAsync();
        }

        public async Task<IEnumerable<ReminderDto>> GetUpcomingRemindersAsync(DateTime date)
        {
            var dateOnly = DateOnly.FromDateTime(date); 

            return await _context.Remenders
                .Where(r => r.ReminderDate > dateOnly) 
                .Select(r => new ReminderDto
                {
                    Id = r.Id,
                    BookId = r.BookId,
                    UserId = r.UserId,
                    ReminderDate = r.ReminderDate.ToDateTime(TimeOnly.MinValue), 
                    ReminderMessage = r.ReminderMessage
                })
                .ToListAsync();
        }

        public async Task<bool> RemoveReminderAsync(int reminderId)
        {
            var reminder = await _context.Remenders.FindAsync(reminderId);
            if (reminder == null)
                return false;

            _context.Remenders.Remove(reminder);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
