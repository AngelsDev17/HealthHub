using Angels.Packages.JwtToken.Services;
using Angels.Packages.Logger.Services;
using HealthHub.Application.Dtos.AuthService;
using HealthHub.Application.Dtos.UserManagement;
using HealthHub.Application.Interfaces.AuthService;
using HealthHub.Application.Interfaces.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthHub.WebApi.Controllers
{
    [ApiExplorerSettings(GroupName = "profile-page")]
    [Tags("Administración del pefil")]
    [Route("profile-service"), ApiController, Authorize(Roles = "User,Administrator")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        private readonly IUserManagementService _userManagementService;

        public ProfileController(
            ILogger<AuthController> logger,
            IAuthService authService,
            IUserManagementService userManagementService)
        {
            _logger = logger;
            _authService = authService;
            _userManagementService = userManagementService;
        }


        [HttpPut("update-profile-data")]
        public async Task<IActionResult> UpdateProfileData(UserToUpdateDto userToUpdate)
        {
            try
            {
                userToUpdate.Id = Request.GetUserIdFromBearerToken();

                await _userManagementService.UpdateProfileData(userToUpdate: userToUpdate);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-email")]
        public async Task<IActionResult> UpdateEmail(UserToUpdateEmailDto userToUpdateEmail)
        {
            try
            {
                userToUpdateEmail.Id = Request.GetUserIdFromBearerToken();

                await _authService.UpdateEmail(userToUpdateEmail: userToUpdateEmail);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("confirm-update-email")]
        public async Task<IActionResult> ConfirmUpdateEmail(UpdateEmailDto resetPassword)
        {
            try
            {
                await _authService.ConfirmUpdateEmail(resetPassword: resetPassword);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword(UserToUpdatePasswordDto userToUpdatePassword)
        {
            try
            {
                userToUpdatePassword.Id = Request.GetUserIdFromBearerToken();

                await _authService.UpdatePassword(userToUpdatePassword: userToUpdatePassword);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
