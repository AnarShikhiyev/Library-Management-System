using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Models;

namespace ProjectLibrary_Back.Interfaces
{
    public interface IReportService
    {    // Kirayə statistikalarını yaradır.
        Task<Report> generateRentalStatisticsAsync();

        // Ən çox oxunan kitabları əldə edir
        Task<IEnumerable<GetMostReadBooksDTO>> getMostReadBooksAsync();

        // İstifadəçi fəaliyyətinin hesabatını yaradır.
        Task<Report> generateUserActivityReportAsync();

        // getBookRentalHistory: Kitabın kirayə tarixçəsini əldə edir.
        Task<IEnumerable<GetBookRentalHistoryDTO>> getBookRentalHistoryAsync(int bookId);
        Task<IEnumerable<GetUserLoginHistoryDTO>> getUserLoginHistoryAsync(int userId);


    }
}
