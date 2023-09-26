using BVBM.API.Data;
using BVBM.API.Interface;
using BVBM.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BVBM.API.Repository
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

        public async Task<ICollection<Review>> GetAllReviewsValidatedAsync()
        {
            return await _context.Reviews.Where(i => i.IsValidated == true).OrderByDescending(i => i.CreatedDate).ToListAsync();
        }

        public async Task<ICollection<Review>> GetAllReviewsNotValidatedAsync()
        {
            return await _context.Reviews.Where(i => i.IsValidated == false).OrderByDescending(i => i.CreatedDate).ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(i => i.Id == id);
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

