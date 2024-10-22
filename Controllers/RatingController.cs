using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Exceptions;
using ProjectLibrary_Back.Interfaces;

namespace ProjectLibrary_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase

    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        [Authorize(Roles = "Superadmin")]
        [HttpGet("getUserRating")]
        public async Task<IActionResult> GetUserRatingAsync([FromQuery] int userId, [FromQuery] int bookId)
        {
            try
            {
                var rating = await _ratingService.getUserRatingAsync(userId, bookId);
                return Ok(rating);
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }
        }

        [HttpGet("getAverageRating")]
        public async Task<IActionResult> GetAverageRating([FromQuery] int bookId)
        {
            try
            {
                var averageRating = await _ratingService.getAverageRatingAsync(bookId);
                return Ok(averageRating);
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }

        }
        [Authorize(Roles = "User")]
        [HttpPost("addRating")]
        public async Task<IActionResult> AddRatingAsync([FromBody] AddRatingDTO rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newRating = await _ratingService.addRatingAsync(rating);
                return Ok(newRating);
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }
        }
        [Authorize(Roles = "Superadmin")]
        [HttpPut("updateRating/{ratingId}")]
        public async Task<IActionResult> UpdateRatingAsync(int ratingId, [FromBody] UpdateRatingDTO newRating)
        {
            try
            {
                var success = await _ratingService.updateRatingAsync(ratingId, newRating);
                if (!success)
                {
                    throw new LibraryManagementSystemException("Reytinq tapılmadı.");
                }
                else
                {
                    return Ok("Reytinq dəyişdirildi");
                }
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }
        }
        [Authorize(Roles = "Superadmin")]
        [HttpDelete("deleteRating/{ratingId}")]
        public async Task<IActionResult> DeleteRatingAsync(int ratingId)
        {
            try
            {
                var success = await _ratingService.deleteRatingAsync(ratingId);

                if (!success)
                {
                    throw new LibraryManagementSystemException("Reytinq tapılmadı.");
                }
                else
                {
                    return Ok("Reytinq silindi.");
                }

            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }
        }
        [Authorize(Roles = "Superadmin")]
        [HttpGet("getRatingsAndReviewsByBook/{bookId}")]
        public async Task<IActionResult> GetRatingsAndReviewsByBookAsync(int bookId)
        {
            try
            {
                var ratingAndReview = await _ratingService.getRatingsAndReviewsByBookAsync(bookId);
                return Ok(ratingAndReview);
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }
        }
        [Authorize(Roles = "Superadmin")]
        [HttpGet("getRatingsAndReviewsByUser/{userId}")]
        public async Task<IActionResult> GetRatingsAndReviewsByUserAsync(int userId)
        {
            try
            {
                var ratingAndReview = await _ratingService.getRatingsAndReviewsByUserAsync(userId);
                return Ok(ratingAndReview);
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }
        }
        [Authorize(Roles = "User")]
        [HttpPost("addRatingAndReview")]
        public async Task<IActionResult> AddRatingAndReviewAsync([FromBody] AddRatingAndReviewDTO ratingAndReview)
        {
            try
            {
                var newRatingAndReview = await _ratingService.addRatingAndReviewAsync(ratingAndReview);
                return Ok(newRatingAndReview);
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }
        }

        [HttpPut("updateRatingAndReview")]
        public async Task<IActionResult> UpdateRatingAndReviewAsync([FromQuery] int bookId, [FromQuery] int userId, [FromBody] UpdateRatingAndReviewDTO newUpdateRatingAndReview)
        {
            try
            {
                var result = await _ratingService.updateRatingAndReviewAsync(bookId, userId, newUpdateRatingAndReview);

                if (!result)
                {
                    throw new LibraryManagementSystemException("Reytinq və ya rəy tapılmadı.");
                }

                return Ok("Reytinq və rəy uğurla yeniləndi.");
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }
        }
        [Authorize(Roles = "Superadmin")]
        [HttpDelete("deleteRatingAndReview")]
        public async Task<IActionResult> DeleteRatingAndReviewAsync([FromQuery] int bookId, [FromQuery] int userId)
        {
            try
            {
                var result = await _ratingService.deleteRatingAndReviewAsync(bookId, userId);

                if (!result)
                {
                    throw new LibraryManagementSystemException("Reytinq və ya rəy tapılmadı.");
                }

                return Ok("Reytinq və rəy uğurla silindi.");
            }
            catch (LibraryManagementSystemException ex)
            {
                return BadRequest($"Error Message : {ex.Message}");
            }

        }
    }
}
