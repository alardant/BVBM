using BVBM.API.Models;

namespace BVBM.API.Interface
{
    public interface IReviewRepository
    {
        Task<ICollection<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        bool ReviewExists(int id);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool Save();
    }
}
