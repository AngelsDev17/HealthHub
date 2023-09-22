using Angels.Packages.Logger.Services;
using HealthHub.Application.Interfaces.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthHub.WebApi.Controllers
{
    [ApiExplorerSettings(GroupName = "management-page")]
    [Tags("Administración de usuarios")]
    [Route("user-management-service"), ApiController, Authorize(Roles = "Administrator")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserManagementService _userManagementService;

        public UserController(
            ILogger<UserController> logger,
            IUserManagementService userManagementService)
        {
            _logger = logger;
            _userManagementService = userManagementService;
        }


        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userManagementService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
