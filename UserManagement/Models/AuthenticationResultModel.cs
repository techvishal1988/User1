namespace UserManagement.Models
{
    public class AuthenticationResultModel
    {
        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string IdToken { get; set; }

        public string NewDeviceMetadata { get; set; }

        public string RefreshToken { get; set; }

        public string TokenType { get; set; }
    }
}
