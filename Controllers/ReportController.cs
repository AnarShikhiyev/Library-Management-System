using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary_Back.Interfaces;

namespace ProjectLibrary_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController:ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [Authorize(Roles = "Superadmin")]
        [HttpGet("getMostReadBooks")]
        public async Task<IActionResult> GetMostReadBooks()
        {
            var books = await _reportService.getMostReadBooksAsync();
            return Ok(books);
        }
        [Authorize(Roles = "Superadmin,Admin")]
        [HttpGet("getBookRentalHistory/{bookId}")]
        public async Task<IActionResult> GetBookRentalHistory(int bookId)
        {
            var history = await _reportService.getBookRentalHistoryAsync(bookId);
            return Ok(history);
        }
        [Authorize(Roles = "Superadmin,Admin")]
        [HttpPost("generateRentalStatistics")]
        public async Task<IActionResult> GenerateRentalStatisticsAsync()
        {
            var rentalStatistics = await _reportService.generateRentalStatisticsAsync();
            return Ok(rentalStatistics);
        }
        [Authorize(Roles = "Superadmin")]
        [HttpPost("generateUserActivityReport")]
        public async Task<IActionResult> GenerateUserActivityReportAsync()
        {
            var userActivity = await _reportService.generateUserActivityReportAsync();
            return Ok(userActivity);
        }
        [Authorize(Roles = "Superadmin")]
        [HttpGet("getUserLoginHistory")]
       
        public async Task<IActionResult> GetUserLoginHistoryAsync([FromQuery] int userId)
        {

            var userLoginHistory = await _reportService.getUserLoginHistoryAsync(userId);


            if (userLoginHistory == null || !userLoginHistory.Any())
            {
                return NotFound($"ID-si {userId} olan istifadəçi ya mövcud deyil, ya da login tarixçəsi yoxdur.");
            }

            return Ok(userLoginHistory);
        }
    }
}
