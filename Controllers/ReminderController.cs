using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Interfaces;

namespace ProjectLibrary_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;

        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }
        [Authorize(Roles = "Superadmin,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddReminder([FromBody] AddReminderDto dto)
        {
            var result = await _reminderService.AddReminderAsync(dto);
            if (result)
                return CreatedAtAction(nameof(GetRemindersByUser), new { userId = dto.UserId }, dto);

            return BadRequest("Error adding reminder.");
        }
        [Authorize(Roles = "Superadmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveReminder(int id)
        {
            var result = await _reminderService.RemoveReminderAsync(id);
            if (result)
                return NoContent();

            return NotFound();
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetRemindersByUser(int userId)
        {
            var reminders = await _reminderService.GetRemindersByUserAsync(userId);
            return Ok(reminders);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("upcoming/{date}")]
        public async Task<IActionResult> GetUpcomingReminders(DateTime date)
        {
            var reminders = await _reminderService.GetUpcomingRemindersAsync(date);
            return Ok(reminders);
        }
    }
}
