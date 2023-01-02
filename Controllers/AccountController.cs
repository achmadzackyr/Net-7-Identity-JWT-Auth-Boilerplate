using App1.DTOs.Account.profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("profile")]
        [Authorize]
        public async Task<ActionResult> Profile()
        {
            var response = new ProfileResponse();
            var user = await _userManager.FindByEmailAsync(User.Identity?.Name);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
                response.Data = null;
                return NotFound(response);
            }

            response.Success = true;
            response.Message = "User found";
            response.Data = new ProfileData
            {
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                Username = user.UserName
            };

            return Ok(response);
        }
    }
}
