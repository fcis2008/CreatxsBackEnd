using Application.DTOs;
using Application.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<string> RegisterMerchantAsync(RegisterMerchantDto dto)
        {
            var merchant = new Merchant
            {
                UserName = dto.Email,
                Email = dto.Email,
                UserType = UserType.Merchant,
                StoreName = dto.StoreName
            };

            var result = await _userManager.CreateAsync(merchant, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(merchant);

            var confirmationLink = GenerateConfirmationLink(merchant.Id, token);
            //await _emailService.SendEmailAsync(dto.Email, "Confirm Your Email", $"Please confirm your email by clicking <a href=\"{confirmationLink}\">here</a>.");

            return "Merchant registered successfully. Please confirm your email." + Environment.NewLine + confirmationLink;
        }

        public async Task<string> RegisterEndUserAsync(RegisterEndUserDto dto)
        {
            var endUser = new EndUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                UserType = UserType.EndUser
            };

            var result = await _userManager.CreateAsync(endUser, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(endUser);
            var confirmationLink = GenerateConfirmationLink(endUser.Id, token);
            //await _emailService.SendEmailAsync(dto.Email, "Confirm Your Email", $"Please confirm your email by clicking <a href=\"{confirmationLink}\">here</a>.");

            return "EndUser registered successfully. Please confirm your email.";
        }

        public async Task<string> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new Exception("Invalid email or password.");

            if (!user.EmailConfirmed)
                throw new Exception("Please confirm your email before logging in.");

            return GenerateJwtToken(user);
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new Exception("Invalid user.");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                throw new Exception("Email confirmation failed.");

            if(user.UserType == UserType.Merchant)
                await _userManager.AddToRoleAsync(user, "Merchant");
            else 
                await _userManager.AddToRoleAsync(user, "EndUser");

            return "Email confirmed successfully.";
        }

        public async Task ResendEmailConfirmationAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? throw new Exception("User not found.");

            if (user.EmailConfirmed)
                throw new Exception("Email is already confirmed.");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = GenerateConfirmationLink(user.Id, token);
            //await _emailService.SendEmailAsync(email, "Confirm Your Email", $"Please confirm your email by clicking <a href=\"{confirmationLink}\">here</a>.");
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateConfirmationLink(string userId, string token)
        {
            return _configuration["AppSettings:AppUrl"] + $"/api/auth/ConfirmEmail?userId={userId}&token={token}";
        }

        public async Task ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return; // Don't reveal user non-existence for security.

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = GeneratePasswordResetLink(user.Id, token);
            await _emailService.SendEmailAsync(dto.Email, "Reset Your Password", $"Please reset your password by clicking <a href=\"{resetLink}\">here</a>.");
        }

        private string GeneratePasswordResetLink(string userId, string token)
        {
            return _configuration["AppSettings:AppUrl"] + $"/api/auth/ResetPassword?userId={userId}&token={token}";
        }
        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId) ?? throw new Exception("Invalid user.");
            var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
