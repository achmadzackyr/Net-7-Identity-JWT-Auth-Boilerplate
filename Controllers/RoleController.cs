using App1.DTOs.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace App1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<ActionResult> Create([Required] string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
            if (result.Succeeded)
            {
                return Ok();
            } else
            {
                return BadRequest();
            }
            
        }

        [HttpGet("list")]
        public ActionResult Get()
        {
            var response = new RoleResponse
            {
                Success = true,
                Message = "Role list",
                Data = _roleManager.Roles
            };
            return Ok(response);
        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
