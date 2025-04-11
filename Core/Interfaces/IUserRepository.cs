using Core.Models;

namespace Core.Interfaces
{
    public interface IUserRepository<T> where T : ApplicationUser
    {
        Task RegisterAsync(T user, string password);
        Task<T?> GetByEmailAsync(string email);
    }
}
