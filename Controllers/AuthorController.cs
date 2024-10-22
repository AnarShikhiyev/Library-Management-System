using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Exceptions;
using ProjectLibrary_Back.Interfaces;

namespace ProjectLibrary_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [Authorize(Roles = "Superadmin")]
        [HttpPost("addAuthor")]
        public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorDto newAuthor)
        {
            try
            {
                if (newAuthor == null)
                {
                    throw new LibraryManagementSystemException("Müəllif məlumatı boşdur.");
                }

                var result = await _authorService.AddAuthorAsync(newAuthor);
                return CreatedAtAction(nameof(GetAuthorById), new { id = result.Id }, result);
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

        [Authorize(Roles = "Superadmin,Admin")]
        [HttpPut("updateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorDto updatedAuthor)
        {
            try
            {
                if (id <= 0 || updatedAuthor == null)
                {
                    throw new LibraryManagementSystemException("ID və ya yenilənmiş müəllif məlumatları yanlışdır.");
                }

                var result = await _authorService.UpdateAuthorAsync(id, updatedAuthor);
                if (!result)
                {
                    throw new LibraryManagementSystemException($"ID-si {id} olan müəllif tapılmadı.");
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
        [HttpDelete("deleteAuthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var result = await _authorService.DeleteAuthorAsync(id);
                if (!result)
                {
                    throw new LibraryManagementSystemException($"ID-si {id} olan müəllif tapılmadı.");
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

        [Authorize(Roles = "Superadmin,Admin")]
        [HttpGet("getAuthorById/{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            try
            {
                var result = await _authorService.GetAuthorByIdAsync(id);
                if (result == null)
                {
                    throw new LibraryManagementSystemException($"ID-si {id} olan müəllif tapılmadı.");
                }

                return Ok(result);
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

        [Authorize(Roles = "Superadmin,Admin")]
        [HttpGet("getAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var result = await _authorService.GetAllAuthorsAsync();
                if (result == null || !result.Any())
                {
                    throw new LibraryManagementSystemException("Heç bir müəllif tapılmadı.");
                }

                return Ok(result);
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

        [Authorize(Roles = "Superadmin,Admin")]
        [HttpGet("getAuthorByName")]
        public async Task<IActionResult> GetAuthorByName([FromQuery] string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new LibraryManagementSystemException("Müəllif adı boşdur.");
                }

                var result = await _authorService.GetAuthorByNameAsync(name);
                if (result == null)
                {
                    throw new LibraryManagementSystemException($"Adı '{name}' olan müəllif tapılmadı.");
                }

                return Ok(result);
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
        [HttpDelete("removeBookFromAuthor")]
        public async Task<IActionResult> RemoveBookFromAuthor([FromQuery] int authorId, [FromQuery] int bookId)
        {
            try
            {
                var result = await _authorService.RemoveBookFromAuthorAsync(authorId, bookId);
                if (!result)
                {
                    throw new LibraryManagementSystemException($"Müəllifdin kitabi silinmədi. AuthorId: {authorId}, BookId: {bookId}.");
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

        [Authorize(Roles = "Superadmin,Admin")]
        [HttpGet("getBooksByAuthor/{authorId}")]
        public async Task<IActionResult> GetBooksByAuthor(int authorId)
        {
            try
            {
                var result = await _authorService.GetBooksByAuthorAsync(authorId);
                if (result == null)
                {
                    throw new LibraryManagementSystemException($"ID-si {authorId} olan müəllifin heç bir kitabı tapılmadı.");
                }

                return Ok(result);
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
