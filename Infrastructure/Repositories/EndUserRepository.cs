using Core.Interfaces;
using Core.Models;

namespace Infrastructure.Repositories
{
    public class EndUserRepository : IEndUserRepository
    {
        public Task AddAsync(EndUser endUser)
        {
            throw new NotImplementedException();
        }

        public Task<EndUser> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<EndUser> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EndUser endUser)
        {
            throw new NotImplementedException();
        }
    }
}
