using BVBM.API.Data;
using BVBM.API.Interface;
using BVBM.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BVBM.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.OrderByDescending(i => i.CreatedDate).ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool CreateReview(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public bool ReviewExists(int id)
        {
            return _context.Reviews.Any(c => c.Id == id);
        }


    }
}