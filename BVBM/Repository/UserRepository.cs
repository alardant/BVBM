using BVBM.Data;
using BVBM_API.Interface;

namespace BVBM_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public bool UserExists(int id)
        {
            return _context.Reviews.Any(c => c.Id == id);
        }
    }
}
