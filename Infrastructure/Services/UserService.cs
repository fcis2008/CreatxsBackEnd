using Application.DTOs;
using Core.Models;
using Creatxs.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailSender;

        public UserService(UserManager<ApplicationUser> userManager, IEmailService emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;
            
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }

        public async Task<IdentityResult> RegisterEndUserAsync(RegisterEndUserDto dto)
        {
            var endUser = new EndUser
            {
                UserName = dto.Email,
                Email = dto.Email,
            };

            var result = await _userManager.CreateAsync(endUser, dto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(endUser, "EndUser");

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(endUser);
                var confirmationLink = $"https://yourdomain.com/api/v1/account/confirmemail?userId={endUser.Id}&token={Uri.EscapeDataString(token)}";

                await _emailSender.SendEmailAsync(dto.Email, "Confirm your email", $"Please confirm your account by clicking <a href='{confirmationLink}'>here</a>.");
            }

            return result;
        }

        public async Task<IdentityResult> RegisterMerchantAsync(RegisterMerchantDto dto)
        {
            var merchant = new Merchant
            {
                UserName = dto.Email,
                Email = dto.Email,
                StoreName = dto.StoreName
            };

            var result = await _userManager.CreateAsync(merchant, dto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(merchant, "Merchant");

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(merchant);
                var confirmationLink = $"https://yourdomain.com/api/v1/account/confirmemail?userId={merchant.Id}&token={Uri.EscapeDataString(token)}";

                await _emailSender.SendEmailAsync(dto.Email, "Confirm your email", $"Please confirm your account by clicking <a href='{confirmationLink}'>here</a>.");
            }

            return result;
        }
    }
}
