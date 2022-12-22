using System.ComponentModel.DataAnnotations;

namespace App1.DTOs.Login
{
    public class LoginRequest
    {
        [RegularExpression(@"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$",
            ErrorMessage = "Username & password must a valid base 64 string")]
        public string Username { get; set; }

        [RegularExpression(@"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$",
            ErrorMessage = "Username & password must a valid base 64 string")]
        public string Password { get; set; }
    }
}
