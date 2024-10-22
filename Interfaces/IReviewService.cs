using ProjectLibrary_Back.DTO;

namespace ProjectLibrary_Back.Interfaces
{
    public interface IReviewService
    {

        Task<AddReviewDTO> addReviewAsync(AddReviewDTO review);

        Task<bool> updateReviewAsync(int reviewId, UpdateReviewDTO newReview);


        Task<bool> deleteReviewAsync(int reviewId);


        Task<GetAllReviewsForBookDTO> getAllReviewsForBookAsync(int bookId);

        Task<IEnumerable<GetUserReviewDTO>> getUserReviewAsync(int userId, int bookId);

    }
}
