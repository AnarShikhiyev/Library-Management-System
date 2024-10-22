using ProjectLibrary_Back.DTO;

namespace ProjectLibrary_Back.Interfaces
{
    public interface IReservationService
    {
        Task<bool> AddReservationAsync(AddReservationDto dto);
        Task<bool> UpdateReservationAsync(int id, UpdateReservationDto dto);
        Task<bool> CancelReservationAsync(int id);
        Task<IEnumerable<ReservationDto>> GetUserReservationsAsync(int userId);
        Task<IEnumerable<ReservationDto>> GetBookReservationsAsync(int bookId);
        Task<ReservationDto> GetReservationDetailsAsync(int id);
        Task<bool> CheckAvailabilityAsync(int bookId, DateTime startDate, DateTime endDate);
    }
}
