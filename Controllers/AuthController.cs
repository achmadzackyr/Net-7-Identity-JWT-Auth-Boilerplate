﻿using App1.DTOs.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var response = new LoginResponse();
            
            var userData = await _userManager.FindByEmailAsync(request.Username);
            if (!userData.EmailConfirmed)
            {
                response.Message = "Email not confirmed yet";
                response.Success = false;
                response.Data = null;
                return StatusCode(500, response);
            }
            bool loginSuccess = await _userManager.CheckPasswordAsync(userData, request.Password);
            if (!loginSuccess)
            {
                response.Message = "Password is incorrect";
                response.Success = false;
                response.Data = null;
                return StatusCode(500, response);
            }

            var token = await GenerateToken(userData);
            return Ok(token);
        }

        private async Task<TokenResponse> GenerateToken(IdentityUser userData)
        {
            var response = new TokenResponse();

            if (userData != null)
            {
                var userRoles = await _userManager.GetRolesAsync(userData);

                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userData?.UserName),
                        new Claim("Email", userData?.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);
                var exp = token.ValidTo;
                response = new TokenResponse
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiresIn = Convert.ToInt64(exp.Subtract(exp.AddYears(-1)).TotalSeconds),
                    Email = userData.Email,
                    Issued = string.Format("{0:ddd, dd MMM yyyy HH:mm:ss} GMT", exp.AddYears(-1).ToUniversalTime()),
                    Expires = string.Format("{0:ddd, dd MMM yyyy HH:mm:ss} GMT", exp.ToUniversalTime())
                };
            }

            return response;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                //issuer: _configuration["JWT:ValidIssuer"],
                //audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddYears(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}