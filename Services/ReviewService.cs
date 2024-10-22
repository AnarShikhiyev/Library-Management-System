using Microsoft.EntityFrameworkCore;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Exceptions;
using ProjectLibrary_Back.Interfaces;
using ProjectLibrary_Back.Models;
using ProjectLibrary_Back.Validation;

namespace ProjectLibrary_Back.Services
{
    public class ReviewService : IReviewService

    {
        private readonly LibraryProjectContext _context;
        public ReviewService(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<AddReviewDTO> addReviewAsync(AddReviewDTO review)
        {
            if (string.IsNullOrEmpty(review.ReviewText) || string.IsNullOrWhiteSpace(review.ReviewText))
            {
                throw new LibraryManagementSystemException("Rəy düzgün verilməyib.");
            }

            IsValid.IsValidId(review.BookId);
            IsValid.IsValidId(review.UserId);

            var book = await _context.Books.AnyAsync(b => b.Id == review.BookId);
            var user = await _context.Users.AnyAsync(u => u.Id == review.UserId);

            if (!book && !user)
            {
                throw new LibraryManagementSystemException($"Id-si {review.BookId} olan kitab və Id-si {review.UserId} olan istifadəçi tapılmadı.");
            }

            if (!book)
            {
                throw new LibraryManagementSystemException($"Id-si {review.BookId} olan kitab tapılmadı.");
            }

            if (!user)
            {
                throw new LibraryManagementSystemException($"Id-si {review.UserId} olan istifadəçi tapılmadı.");
            }

            var newReview = new Review
            {
                ReviewText = review.ReviewText,
                ReviewDate = DateTime.Now.Date,
                BookId = review.BookId,
                UserId = review.UserId
            };


            await _context.Reviews.AddAsync(newReview);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> deleteReviewAsync(int reviewId)
        {
            IsValid.IsValidId(reviewId);

            var review = await _context.Reviews.FindAsync(reviewId);

            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<GetAllReviewsForBookDTO> getAllReviewsForBookAsync(int bookId)
        {
            IsValid.IsValidId(bookId);

            var reviews = await _context.Books
                .Include(r => r.Reviews)
                .SingleOrDefaultAsync(r => r.Id == bookId);

            if (reviews is null)
            {
                throw new LibraryManagementSystemException("Kitab tapılmadı.");
            }

            else
            {
                var allReviewsForBook = new GetAllReviewsForBookDTO
                {

                    BookId = reviews.Id,
                    BookTitle = reviews.BookTitle,
                    Reviews = reviews.Reviews.Select(r => r.ReviewText).ToList()
                };

                if (!allReviewsForBook.Reviews.Any())
                {
                    throw new LibraryManagementSystemException($"Id-si {bookId} olan kitaba rəy yazılmayıb.");
                }
                return allReviewsForBook;
            }
        }

        public async Task<IEnumerable<GetUserReviewDTO>> getUserReviewAsync(int userId, int bookId)
        {
            IsValid.IsValidId(bookId);
            IsValid.IsValidId(userId);

            var book = await _context.Books.AnyAsync(b => b.Id == bookId);
            var user = await _context.Users.AnyAsync(u => u.Id == userId);

            if (!book && !user)
            {
                throw new LibraryManagementSystemException($"Id-si {bookId} olan kitab və Id-si {userId} olan istifadəçi tapılmadı.");
            }

            if (!book)
            {
                throw new LibraryManagementSystemException($"Id-si {bookId} olan kitab tapılmadı.");
            }

            if (!user)
            {
                throw new LibraryManagementSystemException($"Id-si {userId} olan istifadəçi tapılmadı.");
            }

            var review = await _context.Reviews
                .Include(r => r.Book)
                .Include(r => r.User)
                .Where(r => r.UserId == userId && r.BookId == bookId)
                .Select(r => new GetUserReviewDTO
                {
                    UserName = r.User.Username,
                    BookTitle = r.Book.BookTitle,
                    ReviewText = r.ReviewText,
                    ReviewDate = r.ReviewDate
                }).ToListAsync();

            if (!review.Any())
            {
                throw new LibraryManagementSystemException($"Id-si {userId} olan istifadəçi Id-si {bookId} olan kitaba rəy yazmayıb.");
            }
            else
            {
                return review;
            }
        }

        public async Task<bool> updateReviewAsync(int reviewId, UpdateReviewDTO newReview)
        {
            IsValid.IsValidId(reviewId);

            if (string.IsNullOrEmpty(newReview.ReviewText) || string.IsNullOrWhiteSpace(newReview.ReviewText))
            {
                throw new LibraryManagementSystemException("Rəy düzgün daxil edilməyib.");
            }

            var review = await _context.Reviews
                .SingleOrDefaultAsync(r => r.Id == reviewId);

            if (review != null)
            {
                review.ReviewText = newReview.ReviewText;
                review.ReviewDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
