using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Exceptions;
using ProjectLibrary_Back.Interfaces;

namespace ProjectLibrary_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase


    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [Authorize(Roles = "Superadmin")]
        [HttpGet("getAllReviewsForBook")]
        public async Task<IActionResult> GetAllReviewsForBookAsync([FromQuery] int bookId)
        {
            try
            {
                var allReviewsForBook = await _reviewService.getAllReviewsForBookAsync(bookId);
                return Ok(allReviewsForBook);

            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }
        }
        [Authorize(Roles = "Superadmin")]
        [HttpGet("getUserReview")]
        public async Task<IActionResult> GetUserReviewAsync([FromQuery] int userId, [FromQuery] int bookId)
        {
            try
            {
                var userReview = await _reviewService.getUserReviewAsync(userId, bookId);
                return Ok(userReview);
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }
        }
        [Authorize(Roles = "User")]
        [HttpPost("addReview")]
        public async Task<IActionResult> AddReviewAsync([FromBody] AddReviewDTO review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newReview = await _reviewService.addReviewAsync(review);
                return Ok(newReview);
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }

        }

        [HttpPut("updateReview/{reviewId}")]
        public async Task<IActionResult> UpdateReviewAsync(int reviewId, [FromBody] UpdateReviewDTO newReview)
        {
            try
            {
                var success = await _reviewService.updateReviewAsync(reviewId, newReview);
                if (!success)
                {
                    throw new LibraryManagementSystemException("Rəy tapılmadı.");
                }
                else
                {
                    return Ok("Rəy yeniləndi");
                }
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }
        }
        [Authorize(Roles = "Superadmin")]
        [HttpDelete("deleteReview/{reviewId}")]
        public async Task<IActionResult> DeleteReviewAsync(int reviewId)
        {
            try
            {
                var success = await _reviewService.deleteReviewAsync(reviewId);
                if (!success)
                {
                    throw new LibraryManagementSystemException("Rəy tapılmadı.");
                }
                else
                {
                    return Ok("Rəy silindi.");
                }
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }

        }
    }
}
