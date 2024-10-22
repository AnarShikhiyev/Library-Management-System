using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Interfaces;
using ProjectLibrary_Back.Models;

namespace ProjectLibrary_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IBookRentalService _bookRentalService;

        public RentalController(IBookRentalService bookRentalService)
        {
            _bookRentalService = bookRentalService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("check-overdue-books")]
        public async Task<IActionResult> CheckOverdueBooks(DateTime currentDate)
        {
            var overdueBooks = await _bookRentalService.CheckOverdueBooksAsync(currentDate);
            if (overdueBooks == null || !overdueBooks.Any())
            {
                return NotFound("No overdue books found.");
            }
            return Ok(overdueBooks);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("send-overdue-notices")]
        public async Task<IActionResult> SendOverdueNotices([FromBody] List<Rental> overdueBooks)
        {
            var result = await _bookRentalService.SendOverdueNoticesAsync(overdueBooks);
            if (!result)
            {
                return BadRequest("Failed to send overdue notices.");
            }
            return Ok("Notices sent successfully.");
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("update-book-status")]
        public async Task<IActionResult> UpdateBookStatus(int rentalId)
        {
            var result = await _bookRentalService.UpdateBookStatusAsync(rentalId);
            if (!result)
            {
                return BadRequest("Failed to update book status.");
            }
            return Ok("Book status updated successfully.");
        }

        [HttpPost("log-return-event")]
        public async Task<IActionResult> LogReturnEvent(int bookId, int userId, DateTime returnDate)
        {
            var result = await _bookRentalService.LogReturnEventAsync(bookId, userId, returnDate);
            if (!result)
            {
                return BadRequest("Failed to log return event.");
            }
            return Ok("Return event logged successfully.");
        }
        [Authorize(Roles = "Superadmin,Admin")]
        [HttpPost("addRental")]
        public async Task<IActionResult> AddRental([FromBody] AddRentalDto newRental)
        {
            if (newRental == null)
            {
                return BadRequest("Rental data is null.");
            }

            var result = await _bookRentalService.AddRentalAsync(newRental);
            if (result)
            {
                return CreatedAtAction(nameof(GetRentalById), new { id = newRental.BookId }, newRental);
            }

            return BadRequest("Failed to add rental.");
        }

     
        [Authorize(Roles = "Superadmin,Admin")]
        [HttpPut("updateRental/{rentalId}")]
        public async Task<IActionResult> UpdateRental(int rentalId, [FromBody] UpdateRentalDto updateRental)
        {
            if (updateRental == null || rentalId != updateRental.RentalId)
            {
                return BadRequest("Invalid rental data.");
            }

            var result = await _bookRentalService.UpdateRentalAsync(updateRental);
            if (result)
            {
                return NoContent();
            }

            return NotFound("Rental not found or failed to update.");
        }
        [HttpGet("GetRentalById")]
        public async Task<IActionResult> GetRentalById(int rentalId)
        {
            var rental = await _bookRentalService.GetRentalByIdAsync(rentalId);
            if (rental == null)
            {
                return NotFound("Rental not found.");
            }

            return Ok(rental);
        }
    }
}
