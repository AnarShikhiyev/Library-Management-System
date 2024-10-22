using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Exceptions;
using ProjectLibrary_Back.Interfaces;

namespace ProjectLibrary_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        [Authorize(Roles = "Superadmin,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto newBook)
        {
            try
            {
                if (newBook == null)
                {
                    throw new LibraryManagementSystemException("Kitab məlumatı boşdur.");
                }

                var bookId = await _bookService.AddBookAsync(newBook);
                return CreatedAtAction(nameof(GetBookById), new { id = bookId }, newBook);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookDto updatedBook)
        {
            try
            {
                if (id <= 0 || updatedBook == null)
                {
                    throw new LibraryManagementSystemException("ID və ya yenilənmiş kitab məlumatları yanlışdır.");
                }

                var result = await _bookService.UpdateBookAsync(id, updatedBook);

                if (!result)
                {
                    throw new LibraryManagementSystemException($"ID-si {id} olan kitab tapılmadı.");
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var success = await _bookService.DeleteBookAsync(id);
                if (!success)
                {
                    throw new LibraryManagementSystemException($"ID-si {id} olan kitab tapılmadı.");
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    throw new LibraryManagementSystemException($"ID-si {id} olan kitab tapılmadı.");
                }

                return Ok(book);
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
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                if (books == null || !books.Any())
                {
                    throw new LibraryManagementSystemException("Heç bir kitab tapılmadı.");
                }

                return Ok(books);
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

        [Authorize(Roles = "User")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    throw new LibraryManagementSystemException("Axtarış açar sözü boşdur.");
                }

                var books = await _bookService.SearchBooksAsync(keyword);
                if (!books.Any())
                {
                    throw new LibraryManagementSystemException("Heç bir kitab tapılmadı.");
                }

                return Ok(books);
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

        [Authorize(Roles = "User")]
        [HttpGet("filter")]
        public async Task<IActionResult> FilterBooks([FromQuery] FilterBooksDto filter)
        {
            try
            {
                var books = await _bookService.FilterBooksAsync(filter);
                if (!books.Any())
                {
                    throw new LibraryManagementSystemException("Filtrə uyğun heç bir kitab tapılmadı.");
                }

                return Ok(books);
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
        [HttpGet("inventory/{id}")]
        public async Task<IActionResult> GetBookInventory(int id)
        {
            try
            {
                var inventory = await _bookService.GetBookInventoryAsync(id);
                if (inventory == null)
                {
                    throw new LibraryManagementSystemException($"ID-si {id} olan kitabın inventar məlumatları tapılmadı.");
                }

                return Ok(inventory);
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
