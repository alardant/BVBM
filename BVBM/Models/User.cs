using Microsoft.AspNetCore.Identity;

namespace BVBM.API.Models
{
    public class User : IdentityUser
    {
        public ICollection<Review> Reviews { get; set; }
    }
}
