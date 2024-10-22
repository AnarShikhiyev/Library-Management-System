using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Interfaces;

namespace ProjectLibrary_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> AddReservation([FromBody] AddReservationDto dto)
        {
            var result = await _reservationService.AddReservationAsync(dto);
            if (result)
            {
                return CreatedAtAction(nameof(GetReservationDetails), new { id = dto.BookId }, dto);
            }
            return BadRequest("Failed to add reservation.");
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationDto dto)
        {
            var result = await _reservationService.UpdateReservationAsync(id, dto);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
        [Authorize(Roles = "Superadmin,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var result = await _reservationService.CancelReservationAsync(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserReservations(int userId)
        {
            var reservations = await _reservationService.GetUserReservationsAsync(userId);
            return Ok(reservations);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetBookReservations(int bookId)
        {
            var reservations = await _reservationService.GetBookReservationsAsync(bookId);
            return Ok(reservations);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationDetails(int id)
        {
            var reservation = await _reservationService.GetReservationDetailsAsync(id);
            if (reservation != null)
            {
                return Ok(reservation);
            }
            return NotFound();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("check-availability")]
        public async Task<IActionResult> CheckAvailability(int bookId, DateTime startDate, DateTime endDate)
        {
            var isAvailable = await _reservationService.CheckAvailabilityAsync(bookId, startDate, endDate);
            return Ok(isAvailable);
        }
    }
}
