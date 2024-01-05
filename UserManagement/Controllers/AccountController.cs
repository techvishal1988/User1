using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Services;


namespace UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserManagement _userManagement;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserManagement userManagement, ILogger<AccountController> logger)
        {
            _userManagement = userManagement;
            _logger = logger;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> AccountRegister(RegistrationRequestModel model)
        {
            var result = await _userManagement.AdminCreateUserAsync(model);
            return Ok(result);
        }


        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(LoginRequestModel model)
        {
            var result = await _userManagement.SignIn(model);
            return Ok(result);
        }

        [HttpGet("Test")]
        public async Task<IActionResult> Test()
        {
            return Ok("Test");
        }
    }
}
