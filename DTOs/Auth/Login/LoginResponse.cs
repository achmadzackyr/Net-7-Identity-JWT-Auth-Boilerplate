using Microsoft.AspNetCore.Authorization;

namespace App1.DTOs.Auth.Login
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public TokenResponse? Data { get; set; }
    }

    public class TokenResponse
    {
        public string? Email { get; set; }
        public string? AccessToken { get; set; }
        public long ExpiresIn { get; set; }
        public string Issued { get; set; }
        public string Expires { get; set; }
    }
}
