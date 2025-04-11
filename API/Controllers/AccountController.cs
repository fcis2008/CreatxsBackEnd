using Application.DTOs;
using Application.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService authService)
        {
            _userService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterMerchant([FromBody] RegisterMerchantDto registerMerchantDto)
        {
            var response = await _userService.RegisterMerchantAsync(registerMerchantDto);
            return Ok(response);// "A merchant account has been successfully. Please confirm your email");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterEndUser([FromBody] RegisterEndUserDto registerEndUserDto)
        {
            // Register the user
            var result = await _userService.RegisterEndUserAsync(registerEndUserDto);
            return Ok(result);// "Your account has been created successfully. Please confirm your email");
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginDTO dto)
        {
            var response = await _userService.LoginAsync(dto);
            return Ok(response);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var result = await _userService.ConfirmEmailAsync(userId, token);
            return result ? Ok("Email confirmed!") : BadRequest("Invalid confirmation.");
        }
    }
}
