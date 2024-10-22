using Microsoft.EntityFrameworkCore;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Interfaces;
using ProjectLibrary_Back.Models;

namespace ProjectLibrary_Back.Services
{
    public class ReportService : IReportService
    {
       private readonly LibraryProjectContext _context;
        public ReportService(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<Report> generateRentalStatisticsAsync()
		{
			var totalRentals = await _context.Rentals.CountAsync();
			var activeRentals = await _context.Rentals.Where(r => r.status == true).CountAsync();
			var returnedBooks = totalRentals - activeRentals;

			var rentalStatistics = new Report
			{
				ReportType = "Rental Statistics",
                GeneratedDate = DateOnly.FromDateTime(DateTime.Now),
                ReportData = $" TotalRentals : {totalRentals}, ActiveRentals : {activeRentals}, ReturnedBooks : {returnedBooks}",
				Description = "İyul ayı üçün kirayə statistikası"
			};

			await _context.Reports.AddAsync(rentalStatistics);
			await _context.SaveChangesAsync();
			return rentalStatistics;
		}

		public async Task<Report> generateUserActivityReportAsync()
		{
			
			var totalUsers = await _context.Users.CountAsync();
			var activeUsers = await _context.Users.Where(r => r.Active == true).CountAsync();
			var inactiveUsers = totalUsers - activeUsers;

			var userActivity = new Report
			{
				ReportType = "User Activity",
                GeneratedDate = new DateOnly(2024, 9, 1),
                ReportData = $"TotalUsers : {totalUsers}, ActiveUsers : {activeUsers}, InactiveUsers : {inactiveUsers}",
				Description = "Sistemdə aktiv və passiv istifadəçilərin fəaliyyəti"
			};

			await _context.Reports.AddAsync(userActivity);
			await _context.SaveChangesAsync();
			return userActivity;
		}

		public async Task<IEnumerable<GetBookRentalHistoryDTO>> getBookRentalHistoryAsync(int bookId)
		{
			return await _context.Rentals.Where(r => r.BookId == bookId)
				.Select(r => new GetBookRentalHistoryDTO
				{
					UserId = r.UserId,
					RentalDate = r.RentalDate.ToString("yyyy-MM-dd"),
					DueDate = r.DueDate.ToString("yyyy-MM-dd")
				}).ToListAsync();	
		}

		public async Task<IEnumerable<GetMostReadBooksDTO>> getMostReadBooksAsync()
		{
			return await _context.Rentals
				.GroupBy(r => r.BookId)
				.OrderByDescending(r => r.Count())
				.Select(r => new GetMostReadBooksDTO
				{
					BookId = (int)r.Key,
					BookTitle = r.Select(b => b.Book.BookTitle).FirstOrDefault(),
					ReadCount = r.Count()
				})
				.Take(3)
				.ToListAsync();	

		}
		public async Task<IEnumerable<GetUserLoginHistoryDTO>> getUserLoginHistoryAsync(int userId)
		{
			
			var userExists = await _context.Users.AnyAsync(u => u.Id == userId);

			if (!userExists)
			{
				return Enumerable.Empty<GetUserLoginHistoryDTO>(); 
			}

			var userLogins = await _context.UserLogins
				.Include(u => u.User)
				.Where(u => u.UserId == userId)
				.ToListAsync();

			
			if (!userLogins.Any())
			{
				return Enumerable.Empty<GetUserLoginHistoryDTO>();
			}

			
			return userLogins.Select(u => new GetUserLoginHistoryDTO
			{
				UserId = userId,
				UserName = u.User.Username,
				UserLoginDate = u.UserLoginDate
			}).ToList();
		}
        }
}
