using Microsoft.AspNetCore.Identity;

namespace App1.DTOs.Role
{
    public class RoleResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public IQueryable<IdentityRole>? Data { get; set; }
    }
}
