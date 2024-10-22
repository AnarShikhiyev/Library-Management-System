using ProjectLibrary_Back.DTO;

namespace ProjectLibrary_Back.Interfaces
{
    public interface INotificationService
    {
        Task<bool> AddNewBookNotificationAsync(AddNewBookNotificationDto dto);
        Task<bool> AddEventNotificationAsync(AddEventNotificationDto dto);
        Task<bool> SendReminderNotificationAsync(int userId, SendReminderNotificationDto dto);
        Task<IEnumerable<NotificationDto>> GetNotificationsByUserAsync(int userId);
        Task<bool> RemoveNotificationAsync(int notificationId);
    }
}
