using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Models;

namespace ProjectLibrary_Back.Interfaces
{
    public interface IBookRentalService
    {
        Task<List<Rental>> CheckOverdueBooksAsync(DateTime currentDate);
        Task<bool> SendOverdueNoticesAsync(List<Rental> overdueBooks);
        Task<bool> UpdateBookStatusAsync(int rentalId);
        Task<bool> LogReturnEventAsync(int bookId, int userId, DateTime returnDate);
        Task<bool> AddRentalAsync(AddRentalDto rentalDto);
        Task<bool> UpdateRentalAsync(UpdateRentalDto updateDto);
        Task<RentalDto> GetRentalByIdAsync(int rentalId);
    }
}
