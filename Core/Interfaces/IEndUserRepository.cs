using Core.Models;

namespace Core.Interfaces
{
    public interface IEndUserRepository
    {
        Task<EndUser> GetByIdAsync(string id);
        Task<EndUser> GetByEmailAsync(string email);
        Task AddAsync(EndUser endUser);
        Task UpdateAsync(EndUser endUser);
    }
}
