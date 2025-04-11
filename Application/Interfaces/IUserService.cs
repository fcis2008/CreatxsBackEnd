using Application.DTOs;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterMerchantAsync(RegisterMerchantDto dto);
        Task<string> RegisterEndUserAsync(RegisterEndUserDto dto);
        Task<string> ConfirmEmailAsync(string userId, string token);
    }
}
