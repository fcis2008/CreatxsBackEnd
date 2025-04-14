using Microsoft.AspNetCore.Identity;

namespace Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public UserType UserType { get; set; }
    }

    public enum UserType
    {
        Merchant = 1,
        EndUser = 2
    }
}
