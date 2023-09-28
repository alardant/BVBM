using BVBM.API.Models;
using Microsoft.AspNetCore.Identity;

namespace BVBM.API.Interface
{
    public interface IUserRepository
    {
        IdentityUser GetFirstUser();
    }
}
