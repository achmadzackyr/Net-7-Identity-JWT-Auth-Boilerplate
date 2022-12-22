using System.ComponentModel.DataAnnotations;

namespace App1.DTOs.Auth.Login
{
    public class LoginRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
