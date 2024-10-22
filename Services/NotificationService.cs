using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Models;
using Microsoft.EntityFrameworkCore;
using ProjectLibrary_Back.Interfaces;

namespace ProjectLibrary_Back.Services
{
    public class NotificationService : INotificationService
    {
        private readonly LibraryProjectContext _context;

        public NotificationService(LibraryProjectContext context)
        {
            _context = context;
        }
        public async Task<bool> AddEventNotificationAsync(AddEventNotificationDto dto)
        {
            var notification = new Notification
            {
                UserId = dto.UserId,
                EventId = dto.EventId,
                NotificationMessage = dto.NotificationMessage,
                NotificationDate = DateOnly.FromDateTime(DateTime.Now)
            };
            _context.Notifications.Add(notification);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddNewBookNotificationAsync(AddNewBookNotificationDto dto)
        {
            var notification = new Notification
            {
                UserId = dto.UserId,
                BookId = dto.BookId,
                NotificationMessage = dto.NotificationMessage,
                NotificationDate = DateOnly.FromDateTime(DateTime.Now)
            };
            _context.Notifications.Add(notification);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<NotificationDto>> GetNotificationsByUserAsync(int userId)
        {
            return await _context.Notifications
            .Where(n => n.UserId == userId)
            .Select(n => new NotificationDto
            {
                Id = n.Id,
                UserId = n.UserId,
                NotificationMessage = n.NotificationMessage,
                NotificationDate = n.NotificationDate.HasValue
                ? n.NotificationDate.Value.ToDateTime(TimeOnly.MinValue)
                : (DateTime?)null,
                BookId = n.BookId,
                EventId = n.EventId
            })
            .ToListAsync();
        }

        public async Task<bool> RemoveNotificationAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null)
                return false;

            _context.Notifications.Remove(notification);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> SendReminderNotificationAsync(int userId, SendReminderNotificationDto dto)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
               
                return false;
            }

            
            var notification = new Notification
            {
               
                NotificationMessage = dto.Message,
                            };

          
            _context.Notifications.Add(notification);

           
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
