using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterMerchantAsync(RegisterMerchantDto registerMerchantDto);
        Task<string> RegisterEndUserAsync(RegisterEndUserDto registerEndUserDto);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<string> LoginAsync(LoginDTO loginDTO);
        Task ResendEmailConfirmationAsync(string email);
        Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
    }
}
