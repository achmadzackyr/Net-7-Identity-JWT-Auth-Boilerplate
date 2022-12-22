using System.ComponentModel.DataAnnotations;

namespace App1.DTOs.Auth.Register
{
    public class RegisterRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
