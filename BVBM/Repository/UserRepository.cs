using BVBM.API.Data;
using BVBM.API.Interface;
using BVBM.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BVBM.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IdentityUser GetFirstUser()
        {
             return _userManager.Users.FirstOrDefault();
        }
    }
}
