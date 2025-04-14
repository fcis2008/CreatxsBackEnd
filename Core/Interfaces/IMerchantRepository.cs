using Core.Models;

namespace Core.Interfaces
{
    public interface IMerchantRepository
    {
        Task<Merchant> GetByIdAsync(string id);
        Task<Merchant> GetByEmailAsync(string email);
        Task AddAsync(Merchant merchant);
        Task UpdateAsync(Merchant merchant);
    }
}
