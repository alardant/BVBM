using BVBM.API.Models;

namespace BVBM.API.Interface
{
    public interface IReviewRepository
    {
        Task<ICollection<Review>> GetAllReviewsAsync();
        Task<ICollection<Review>> GetAllReviewsValidatedAsync();
        Task<ICollection<Review>> GetAllReviewsNotValidatedAsync();
        Task<Review> GetReviewByIdAsync(int id);
        bool ReviewExists(int id);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool Save();
    }
}
