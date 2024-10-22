using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Exceptions;
using ProjectLibrary_Back.Interfaces;

namespace ProjectLibrary_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
   
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles = "Superadmin")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto newUser)
        {
            try
            {
                if (newUser == null)
                {
                    throw new LibraryManagementSystemException("İstifadəçi məlumatı boşdur.");
                }

                var user = await _userService.AddUserAsync(newUser);
                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Daxili server xətası: " + ex.Message);
            }
        }

        [Authorize(Roles = "Superadmin,User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updatedUser)
        {
            try
            {
                if (id <= 0 || updatedUser == null)
                {
                    throw new LibraryManagementSystemException("ID və ya yenilənmiş istifadəçi məlumatları yanlışdır.");
                }

                var result = await _userService.UpdateUserAsync(id, updatedUser);
                if (!result)
                {
                    throw new LibraryManagementSystemException($"ID-si {id} olan istifadəçi tapılmadı.");
                }

                return NoContent();
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Daxili server xətası: " + ex.Message);
            }
        }

        [Authorize(Roles = "Superadmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (!result)
                {
                    throw new LibraryManagementSystemException($"ID-si {id} olan istifadəçi tapılmadı.");
                }

                return NoContent();
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Daxili server xətası: " + ex.Message);
            }
        }

        [Authorize(Roles = "Superadmin,User")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    throw new LibraryManagementSystemException($"ID-si {id} olan istifadəçi tapılmadı.");
                }

                return Ok(user);
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Daxili server xətası: " + ex.Message);
            }
        }

        [Authorize(Roles = "Superadmin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                if (users == null || !users.Any())
                {
                    throw new LibraryManagementSystemException("Heç bir istifadəçi tapılmadı.");
                }

                return Ok(users);
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Daxili server xətası: " + ex.Message);
            }
        }

        [Authorize(Roles = "Superadmin")]
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new LibraryManagementSystemException("İstifadəçi adı boşdur.");
                }

                var user = await _userService.GetUserByUsernameAsync(username);
                if (user == null)
                {
                    throw new LibraryManagementSystemException($"İstifadəçi adı '{username}' olan istifadəçi tapılmadı.");
                }

                return Ok(user);
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Daxili server xətası: " + ex.Message);
            }
        }

        [Authorize(Roles = "Superadmin")]
        [HttpGet("role/{roleId}")]
        public async Task<IActionResult> GetUsersByRole(int roleId)
        {
            try
            {
                var users = await _userService.GetUsersByRoleAsync(roleId);
                if (!users.Any())
                {
                    throw new LibraryManagementSystemException("Bu rol üçün istifadəçi tapılmadı.");
                }

                return Ok(users);
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Daxili server xətası: " + ex.Message);
            }
        }

        [Authorize(Roles = "Superadmin")]
        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> DeactivateUser(int id)
        {
            try
            {
                var result = await _userService.DeactivateUserAsync(id);
                if (!result)
                {
                    throw new LibraryManagementSystemException($"ID-si {id} olan istifadəçi tapılmadı.");
                }

                return NoContent();
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Daxili server xətası: " + ex.Message);
            }
        }
    }
}
