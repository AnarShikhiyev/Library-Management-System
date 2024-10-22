using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Interfaces;

namespace ProjectLibrary_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [Authorize(Roles = "Superadmin")]
        [HttpPost("book")]
        public async Task<IActionResult> AddNewBookNotification([FromBody] AddNewBookNotificationDto dto)
        {
            var result = await _notificationService.AddNewBookNotificationAsync(dto);
            if (result)
                return CreatedAtAction(nameof(GetNotificationsByUser), new { userId = dto.UserId }, dto);

            return BadRequest("Error adding notification.");
        }
        [Authorize(Roles = "Superadmin")]
        [HttpPost("event")]
        public async Task<IActionResult> AddEventNotification([FromBody] AddEventNotificationDto dto)
        {
            var result = await _notificationService.AddEventNotificationAsync(dto);
            if (result)
                return CreatedAtAction(nameof(GetNotificationsByUser), new { userId = dto.UserId }, dto);

            return BadRequest("Error adding notification.");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("sendReminder")]
        public async Task<IActionResult> SendReminderNotification(int userId,[FromBody] SendReminderNotificationDto dto)
        {
            var result = await _notificationService.SendReminderNotificationAsync(userId,dto);
            if (result)
                return NoContent();

            return BadRequest("Error sending reminder notification.");
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetNotificationsByUser(int userId)
        {
            var notifications = await _notificationService.GetNotificationsByUserAsync(userId);
            return Ok(notifications);
        }
        [Authorize(Roles = "Superadmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveNotification(int id)
        {
            var result = await _notificationService.RemoveNotificationAsync(id);
            if (result)
                return NoContent();

            return NotFound();
        }
    }
}
