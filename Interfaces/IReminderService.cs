using ProjectLibrary_Back.DTO;

namespace ProjectLibrary_Back.Interfaces
{
    public interface IReminderService
    {
        Task<bool> AddReminderAsync(AddReminderDto dto);
        Task<bool> RemoveReminderAsync(int reminderId);
        Task<IEnumerable<ReminderDto>> GetRemindersByUserAsync(int userId);
        Task<IEnumerable<ReminderDto>> GetUpcomingRemindersAsync(DateTime date);

    }
}
