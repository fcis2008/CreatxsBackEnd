using Core.Interfaces;
using Core.Models;

namespace Infrastructure.Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        public Task AddAsync(Merchant merchant)
        {
            throw new NotImplementedException();
        }

        public Task<Merchant> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Merchant> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Merchant merchant)
        {
            throw new NotImplementedException();
        }
    }
}
