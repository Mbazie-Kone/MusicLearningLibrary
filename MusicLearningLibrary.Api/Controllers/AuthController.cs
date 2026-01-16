using Microsoft.AspNetCore.Mvc;
using MusicLearningLibrary.Api.Dtos;
using MusicLearningLibrary.Application.Auth.Commands;
using MusicLearningLibrary.Application.Auth.Interfaces;

namespace MusicLearningLibrary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Dtos.RegisterRequestDto dto, CancellationToken ct)
        {
            try
            {
                var command = new RegisterUserCommand(dto.Email, dto.Password);
                await _authService.RegisterAsync(command, ct);

                return Ok("Registration started. Check your email.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto, CancellationToken ct)
        {
            try
            {
                var command = new LoginCommand(dto.Email, dto.Password);
                var token = await _authService.LoginAsync(command, ct);

                return Ok(new LoginResponseDto { Token = token });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // api/auth/confirm-email
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string token,  CancellationToken ct)
        {
            try
            {
                var command = new ConfirmEmailCommand(token);
                await _authService.ConfirmEmailAsync(command, ct);

                return Ok("Email confirmed successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
