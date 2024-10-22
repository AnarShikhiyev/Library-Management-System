using ProjectLibrary_Back.DTO;

namespace ProjectLibrary_Back.Interfaces
{
    public interface IRatingService
    {

        Task<AddRatingDTO> addRatingAsync(AddRatingDTO rating);


        Task<bool> updateRatingAsync(int ratingId, UpdateRatingDTO newRtaing);


        Task<bool> deleteRatingAsync(int ratingId);


        Task<double> getAverageRatingAsync(int bookId);

        Task<GetUserRatingDTO> getUserRatingAsync(int userId, int bookId);



        Task<AddRatingAndReviewDTO> addRatingAndReviewAsync(AddRatingAndReviewDTO ratingAndReview);


        Task<GetRatingsAndReviewsByBookDTO> getRatingsAndReviewsByBookAsync(int bookId);


        Task<GetRatingsAndReviewsByUserDTO> getRatingsAndReviewsByUserAsync(int userId);


        Task<bool> updateRatingAndReviewAsync(int bookId, int userId, UpdateRatingAndReviewDTO newUpdateRatingAndReview);


        Task<bool> deleteRatingAndReviewAsync(int bookId, int userId);

    }
}
