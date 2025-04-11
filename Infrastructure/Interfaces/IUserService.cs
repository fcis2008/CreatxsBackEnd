using Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Creatxs.Application.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterMerchantAsync(RegisterMerchantDto dto);
        Task<IdentityResult> RegisterEndUserAsync(RegisterEndUserDto dto);
        Task<bool> ConfirmEmailAsync(string userId, string token);
    }
}
