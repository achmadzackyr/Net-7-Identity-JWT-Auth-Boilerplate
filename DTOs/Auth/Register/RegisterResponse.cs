using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace App1.DTOs.Auth.Register
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public IdentityUser? Data { get; set; }
    }
}
