using Angels.Packages.Logger.Services;
using HealthHub.Application.Dtos.AuthService;
using HealthHub.Application.Dtos.Commons;
using HealthHub.Application.Interfaces.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace HealthHub.WebApi.Controllers
{
    [ApiExplorerSettings(GroupName = "auth-page")]
    [Tags("Portal de usuarios")]
    [Route("auth-service"), ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(
            ILogger<AuthController> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }


        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser(UserToRegisterDto userToRegister)
        {
            try
            {
                await _authService.RegisterUser(userToRegister: userToRegister);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("user-activation")]
        public async Task<IActionResult> UserActivation(UserActivationDto userActivation)
        {
            try
            {
                var bearerToken = await _authService.UserActivation(userActivation: userActivation);
                return Ok(bearerToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sign-in-user")]
        public async Task<IActionResult> SignInUser(UserToAuthDto userToAuth)
        {
            try
            {
                var bearerToken = await _authService.SignInUser(userToAuth: userToAuth);
                return Ok(bearerToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(IdentificationDto identification)
        {
            try
            {
                await _authService.ResetPassword(identification: identification);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("confirm-reset-password")]
        public async Task<IActionResult> ConfirmResetPassword(ResetPasswordDto resetPassword)
        {
            try
            {
                await _authService.ConfirmResetPassword(resetPassword: resetPassword);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
