namespace App1.DTOs.Account.profile
{
    public class ProfileResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public ProfileData? Data { get; set; }    
    }

    public class ProfileData
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
